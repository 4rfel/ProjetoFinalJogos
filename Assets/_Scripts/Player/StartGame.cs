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
				NetworkSceneManager.SwitchScene("Hole1");
			}
		}
	}
}
