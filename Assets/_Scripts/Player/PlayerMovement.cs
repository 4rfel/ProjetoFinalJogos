using UnityEngine;
using MLAPI;

public class PlayerMovement : NetworkBehaviour {

	[SerializeField] GameObject forceCombo;
	[SerializeField] GameObject forceIndicator;
	[SerializeField] GameObject forceBar;
	PlayerCamera playerCamera;

	float minForce = 10f;
	float maxForce = 250;
	float dForce = 5f;
	float currentForce;
	float velThreshhold = 0.01f;
	float heightResetPosition = -100f;
	bool rising = true;

	public Vector3 prePosition;

	Rigidbody rb;
	PlayerInfo playerInfo;
	PlayerSoundController soundController;
	PlayerPause playerPause;

	void Start() {
		if (IsLocalPlayer) {
			currentForce = minForce;
			rb = GetComponent<Rigidbody>();
			playerInfo = GetComponent<PlayerInfo>();
			soundController = GetComponent<PlayerSoundController>();
			playerCamera = GetComponent<PlayerCamera>();
			playerPause = GetComponent<PlayerPause>();

		}
	}

	void FixedUpdate() {
		if (IsLocalPlayer) {
			if (rb.velocity.magnitude < velThreshhold) {
				if (!playerCamera.finished.Value && !playerPause.paused && Input.GetMouseButton(0)) {
					forceCombo.SetActive(true);
					if (rising) {
						currentForce += dForce;
					} else {
						currentForce -= dForce;
					}
					currentForce = Mathf.Clamp(currentForce, minForce, maxForce);
					if (currentForce == maxForce) {
						rising = false;
					} else if (currentForce == minForce) {
						rising = true;
					}
					float porcentagem = currentForce / maxForce * forceBar.transform.localScale.x;
					forceIndicator.transform.localPosition = new Vector3(forceIndicator.transform.localPosition.x, porcentagem, forceIndicator.transform.localPosition.z);

				} else if (currentForce != minForce) {
					forceCombo.SetActive(false);
					soundController.PlaySound("hit");
					prePosition = rb.position;
					rb.AddForce(currentForce * transform.forward, ForceMode.VelocityChange);
					currentForce = minForce;
					playerInfo.AddHit();
				}
			}
			if (rb.transform.position.y < heightResetPosition) {
				rb.velocity = Vector3.zero;
				rb.position = prePosition;
			}
		}
	}

	public void HandleResetToLastPosition() {
		if (Input.GetKey(KeyCode.R) && rb.velocity.magnitude < velThreshhold)
			rb.position = prePosition;
	}
}
