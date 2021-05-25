using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;

public class SceneChangeHandler : NetworkBehaviour {

	bool hasSceneChanged = false;

	void Start() {	
		SceneManager.activeSceneChanged += HandleSceneChange;
	}

	private void HandleSceneChange(Scene arg0, Scene arg1) {
		hasSceneChanged = true;
	}

	private void Update() {
		if (IsLocalPlayer) {
			if (hasSceneChanged) {
				GameObject spawnpoint = GameObject.FindGameObjectWithTag("Spawnpoint");
				if (spawnpoint != null) {
					transform.position = spawnpoint.transform.position;
					hasSceneChanged = false;
				}
			}
		}
	}
}
