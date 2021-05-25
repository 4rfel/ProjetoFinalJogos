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

	void Start() {
		currentForce = minForce;
		rb = GetComponent<Rigidbody>();
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
				prePosition = rb.position;
				rb.AddForce(currentForce * transform.forward, ForceMode.VelocityChange);
				currentForce = 0;
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
