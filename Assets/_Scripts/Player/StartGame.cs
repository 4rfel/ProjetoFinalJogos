using UnityEngine;
using MLAPI;
using MLAPI.SceneManagement;
using MLAPI.Messaging;

public class StartGame : NetworkBehaviour {

	void Start() {
		if (IsHost) {
			gameObject.SetActive(true);
			
		} else {
			gameObject.SetActive(false);
		}
	}

	void Update() {
		if (IsHost) {
			if (Input.GetKeyDown(KeyCode.Q)) {
				ResetScoreServerRpc();
				NetworkSceneManager.SwitchScene("Hole1");
			}
		}
	}

	[ServerRpc]
	void ResetScoreServerRpc() {
		ResetScoreClientRpc();
	}

	[ClientRpc]
	void ResetScoreClientRpc() {
		Debug.Log("AAAAAAA");
		GetComponent<PlayerInfo>().ResetScore();
	}
}
