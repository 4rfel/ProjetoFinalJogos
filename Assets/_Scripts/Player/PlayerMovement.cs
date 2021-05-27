using UnityEngine;
using MLAPI;

public class PlayerMovement : NetworkBehaviour {

	float minForce = 10f;
	float maxForce = 500f;
	float dForce = 1f;
	float currentForce;
	float velThreshhold = 0.01f;

	Vector3 prePosition;

	Rigidbody rb;
	PlayerInfo playerInfo;

	void Start() {
		if (IsLocalPlayer) {
			currentForce = minForce;
			rb = GetComponent<Rigidbody>();
			playerInfo = GetComponent<PlayerInfo>();
		}
	}

	private void Update() {
		if (IsLocalPlayer) {
			HandleForce();
			//HandleResetToLastPosition();
		}
	}

	void FixedUpdate() {
		if (IsLocalPlayer) {
			if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.magnitude < velThreshhold) {
				//Debug.Log()
				prePosition = rb.position;
				rb.AddForce(currentForce * transform.forward, ForceMode.VelocityChange);
				//rb.velocity = transform.forward * 100;
				currentForce = 0;
				playerInfo.AddHit();
			}
		}
	}

	void HandleResetToLastPosition() {
		if (Input.GetKey(KeyCode.R) && rb.velocity.magnitude < velThreshhold)
			rb.position = prePosition;
	}

	void HandleForce() {
		currentForce += dForce * Input.GetAxis("Vertical") * Time.timeScale;
		currentForce = Mathf.Clamp(currentForce, minForce, maxForce);
		//Debug.Log(currentForce);
	}
}
