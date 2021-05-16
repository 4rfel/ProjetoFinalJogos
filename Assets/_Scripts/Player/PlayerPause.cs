using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.SceneManagement;
using MLAPI.Transports.PhotonRealtime;


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
			if (paused) {
				Cursor.lockState = CursorLockMode.None;
			} else {
				Cursor.lockState = CursorLockMode.Locked;
			}
		}

		if (IsLocalPlayer) {
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
		yield return new WaitForSeconds(1);

		System.Collections.Generic.List<MLAPI.Connection.NetworkClient> clients = NetworkManager.Singleton.ConnectedClientsList;
		while (clients.Count != 1) {
			NetworkManager.Singleton.DisconnectClient(clients[1].ClientId);
			yield return new WaitForFixedUpdate();
			clients = NetworkManager.Singleton.ConnectedClientsList;
		}
		yield return new WaitForFixedUpdate();
		NetworkManager.Singleton.StopHost();
		SceneManager.LoadScene("Menu");

	}
}
