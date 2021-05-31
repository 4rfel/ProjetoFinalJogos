using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using System.Collections.Generic;

public class PlayerCamera : NetworkBehaviour {

	[SerializeField] GameObject cam;

	[SerializeField] PlayerPause playerPause;

	Vector3 camOriLocalPos;
	Quaternion camOriLocalRot;

	float pitch = 0f;
	float yaw = 0f;
	float mouseSensitivity = 300f;
	float freeCamSpeed = 500f;

	public NetworkVariable<bool> finished = new NetworkVariable<bool>(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.OwnerOnly }, false);

	public bool isFree = false;

	int currentCam = 0;

	void Start() {
		if (IsLocalPlayer) {
			cam.SetActive(true);
			camOriLocalPos = cam.transform.localPosition;
			camOriLocalRot = cam.transform.localRotation;
		}
	}

	public void ResetCam() {
		cam.transform.localRotation = camOriLocalRot;
		cam.transform.localPosition = camOriLocalPos;
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
			if (finished.Value) {
				//isFree = true;
				HandleSpectatePlayer();
			}
		}
	}

	public void Finish() {
		if (IsLocalPlayer) {
			finished.Value = true;
			HandleSpectatePlayer();
		}
	}

	void Handlelook() {
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		if (!isFree) {
			transform.Rotate(Vector3.up * mouseX);
		} else {
			pitch -= mouseY;
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
				ResetCam();
				yaw = 0f;
			}
		}
	}


	void HandleSpectatePlayer() {
		if (IsLocalPlayer && finished.Value) {
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			if (players.Length == 1)
				return;

			List<GameObject> playersNotFinished = new List<GameObject>();
			foreach (GameObject player in players) {
				if (!player.GetComponent<PlayerCamera>().finished.Value) {
					playersNotFinished.Add(player);
				}
			}

			if (playersNotFinished.Count == 0)
				return;

			if (Input.GetKeyDown(KeyCode.D))
				currentCam++;
			else if (Input.GetKeyDown(KeyCode.A))
				currentCam--;

			currentCam %= playersNotFinished.Count;

			Vector3 pos = playersNotFinished[currentCam].transform.position;

			transform.position = new Vector3(pos.x, transform.position.y, pos.z);
		}
	}
}
