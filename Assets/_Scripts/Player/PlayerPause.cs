using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.SceneManagement;


public class PlayerPause : NetworkBehaviour {

	[SerializeField] GameObject pauseCanvas;
	[SerializeField] GameObject tabCanvas;


	public bool paused = false;

	private void Start() {
		pauseCanvas.SetActive(false);
		tabCanvas.SetActive(false);
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
		Debug.Log("switch to menu");
		NetworkSceneManager.SwitchScene("Menu");
		Debug.Log("wait 3 seconds");
		yield return new WaitForFixedUpdate();
		Debug.Log("call HostDisconnectServerRpc");
		HostDisconnectServerRpc();
		Debug.Log("wait 3 seconds");
		yield return new WaitForFixedUpdate();
		Debug.Log("stop host");
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
		Debug.Log("server call DisconnetClientRpc on clients");
		DisconnetClientRpc();
		Debug.Log("finished calling DisconnetClientRpc on clients");

	}

	[ClientRpc]
	void DisconnetClientRpc() {
		if (IsOwner)
			return;
		Debug.Log("stoping client");
		NetworkManager.Singleton.StopClient();
		Debug.Log("client stoped");
	}
}
