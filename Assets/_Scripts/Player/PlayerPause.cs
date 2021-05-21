using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.SceneManagement;
using MLAPI.Transports.PhotonRealtime;
using MLAPI.Connection;


public class PlayerPause : NetworkBehaviour {

	[SerializeField] GameObject pauseCanvas;
	[SerializeField] GameObject tabCanvas;

	[SerializeField] Text roomName;

	public bool paused = false;

	private void Start() {
		pauseCanvas.SetActive(false);
		tabCanvas.SetActive(false);
		PhotonRealtimeTransport transport = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<PhotonRealtimeTransport>();
		roomName.text = transport.RoomName;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			paused = !paused;
			pauseCanvas.SetActive(paused);
			Cursor.visible = paused;
			if (paused) {
				Cursor.lockState = CursorLockMode.None;
			} else {
				Cursor.lockState = CursorLockMode.Locked;
			}
		}
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
			Disconnect();
			Application.Quit();
		}
	}

	public void Disconnect() {
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

	IEnumerator Disconnect_() {

		NetworkSceneManager.SwitchScene("Menu");

		List<NetworkClient> clients = NetworkManager.Singleton.ConnectedClientsList;
		while (clients.Count != 1) {
			yield return new WaitForFixedUpdate();
			foreach(NetworkClient client in clients) {
				if (NetworkManager.Singleton.ServerClientId != client.ClientId) {
					NetworkManager.Singleton.DisconnectClient(client.ClientId);
					break;
				}
			}
			clients = NetworkManager.Singleton.ConnectedClientsList;
			Debug.LogWarning(clients.Count);
		}
		yield return new WaitForSeconds(0.1f);
		NetworkManager.Singleton.StopHost();
	}
}
