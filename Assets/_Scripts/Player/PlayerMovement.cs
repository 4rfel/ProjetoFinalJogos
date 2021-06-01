using UnityEngine;
using MLAPI;

public class PlayerMovement : NetworkBehaviour {

	[SerializeField] GameObject forceCombo;
	[SerializeField] GameObject forceIndicator;
	[SerializeField] GameObject forceBar;

	float minForce = 10f;
	float maxForce = 500f;
	float dForce = 10f;
	float currentForce;
	float velThreshhold = 0.01f;
	bool rising = true;

	public Vector3 prePosition;

	Rigidbody rb;
	PlayerInfo playerInfo;
	PlayerSoundController soundController;

	void Start() {
		if (IsLocalPlayer) {
			currentForce = minForce;
			rb = GetComponent<Rigidbody>();
			playerInfo = GetComponent<PlayerInfo>();
			soundController = GetComponent<PlayerSoundController>();
		}
	}

	void FixedUpdate() {
		if (IsLocalPlayer) {
			if (Input.GetMouseButton(0)) {
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

			} else if(currentForce != minForce) {
				forceCombo.SetActive(false);
				soundController.PlaySound("hit");
				prePosition = rb.position;
				rb.AddForce(currentForce * transform.forward, ForceMode.VelocityChange);
				currentForce = minForce;
				playerInfo.AddHit();
			}
			//Debug.Log(currentForce);

			if(rb.transform.position.y < -100) {
				rb.position = prePosition;
			}
		}
	}

	public void HandleResetToLastPosition() {
		if (Input.GetKey(KeyCode.R) && rb.velocity.magnitude < velThreshhold)
			rb.position = prePosition;
	}
}
