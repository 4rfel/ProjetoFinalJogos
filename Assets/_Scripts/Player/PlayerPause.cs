using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.SceneManagement;

public class PlayerPause : NetworkBehaviour {

	[SerializeField] GameObject pauseCanvas;
	[SerializeField] GameObject tabCanvas;

	[SerializeField] Text roomName;


	public bool paused = false;

	private void Start() {
		pauseCanvas.SetActive(false);
		tabCanvas.SetActive(false);
			MLAPI.Transports.PhotonRealtime.PhotonRealtimeTransport transport = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<MLAPI.Transports.PhotonRealtime.PhotonRealtimeTransport>();
			roomName.text = transport.RoomName;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			paused = !paused;
			pauseCanvas.SetActive(paused);
			if (paused) {
				Cursor.lockState = CursorLockMode.None;
			} else {
				Cursor.lockState = CursorLockMode.Locked;
			}
		}

		if (IsLocalPlayer) {
		}
	}

	public void Disconnet() {
		if (IsLocalPlayer) {
			if (IsHost) {
				IEnumerator coroutine = Disconnect_();
				StartCoroutine(coroutine);
			} else {
				NetworkManager.Singleton.StopClient();
				SceneManager.LoadScene("Menu");
			}
		}
	}

	private IEnumerator Disconnect_() {
		NetworkSceneManager.SwitchScene("Menu");
		yield return new WaitForFixedUpdate();
		HostDisconnectServerRpc();
		yield return new WaitForFixedUpdate();
		NetworkManager.Singleton.StopHost();
	}

	public void Resume() {
		if (IsLocalPlayer) {
			paused = false;
			pauseCanvas.SetActive(false);
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	public void Quit() {
		if (IsLocalPlayer) {
			Disconnet();
			Application.Quit();
		}
	}

	[ServerRpc]
	void HostDisconnectServerRpc() {
		DisconnetClientRpc();
	}

	[ClientRpc]
	void DisconnetClientRpc() {
		if (IsOwner)
			return;
		NetworkManager.Singleton.StopClient();
	}
}
