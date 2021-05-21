using UnityEngine;
using MLAPI;

public class PlayerCamera : NetworkBehaviour {

	[SerializeField] GameObject cam;

	[SerializeField] PlayerPause playerPause;

	Vector3 camOriPos;

	float pitch = 0f;
	float yaw = 0f;
	float mouseSensitivity = 300f;
	float freeCamSpeed = 500f;

	public bool isFree = false;

	void Start() {
		if (IsLocalPlayer) {
			cam.SetActive(true);
		}
	}

	void Update() {
		if (IsLocalPlayer) {
			if (playerPause.paused)
				return;

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

		if (!isFree) {
			transform.Rotate(Vector3.up * mouseX);
		} else {
			pitch -= mouseY;
			cam.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
			yaw += mouseX;
			cam.transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
		}



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
				yaw = 0f;
			} else {
				camOriPos = cam.transform.position;
			}
		}
	}
}
