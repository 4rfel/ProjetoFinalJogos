using UnityEngine;
using MLAPI;

public class PlayerCamera : NetworkBehaviour {

	[SerializeField] GameObject cam;

	Vector3 camOriPos;

	float pitch = 0f;
	float mouseSensitivity = 300f;
	float freeCamSpeed = 00f;

	public bool isFree = false;

	void Start() {
		if (IsLocalPlayer) {
			cam.SetActive(true);
		}
	}

	void Update() {
		if (IsLocalPlayer) {
			Handlelook();
			HandleChangeCamMode();
			if (isFree) {
				HandleCameraFreeMode();
			}
		}
	}

	void Handlelook() {
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		pitch -= mouseY;
		if (!isFree) {
			pitch = Mathf.Clamp(pitch, -25f, 50f);
		}

		cam.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
		transform.Rotate(Vector3.up * mouseX);

	}

	void HandleCameraFreeMode() {
		float foward = Input.GetAxis("Vertical") * freeCamSpeed * Time.deltaTime;
		float sides = Input.GetAxis("Horizontal") * freeCamSpeed * Time.deltaTime;

		cam.transform.position += cam.transform.forward * foward + cam.transform.right * sides;
	}

	void HandleChangeCamMode() {
		if (Input.GetKeyDown(KeyCode.E)) {
			isFree = !isFree;
			if (!isFree) {
				cam.transform.position = camOriPos;
				cam.transform.LookAt(transform);
			} else {
				camOriPos = cam.transform.position;
			}
		}
	}
}
