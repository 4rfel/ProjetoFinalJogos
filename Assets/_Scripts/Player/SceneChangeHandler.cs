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
					GetComponent<SphereCollider>().enabled = true;
					GetComponent<Rigidbody>().useGravity = true;
					GetComponent<Rigidbody>().velocity = Vector3.zero;
					GetComponentInChildren<MeshRenderer>().enabled = true;
					GetComponentInChildren<PlayerCamera>().finished.Value = false;
					GetComponentInChildren<PlayerCamera>().isFree = false;
					GetComponentInChildren<PlayerCamera>().ResetCam();
					hasSceneChanged = false;
				}
			}
		}
	}
}
