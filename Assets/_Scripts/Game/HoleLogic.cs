using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.SceneManagement;

public class HoleLogic : NetworkBehaviour {

	int quantPlayers;
	int quantPlayersFinished = 0;

	[SerializeField] string nextScene;

	void Start() {
		quantPlayers = NetworkManager.Singleton.ConnectedClientsList.Count;
	}

	private void OnTriggerEnter(Collider collider) {

		collider.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		collider.gameObject.GetComponent<Transform>().position = new Vector3(0,-500,0);
		
		quantPlayersFinished++;
		if (quantPlayersFinished == quantPlayers) {
			StartCoroutine(ChangeHole());
		}
	}

	IEnumerator ChangeHole() {
		print(SceneManager.GetActiveScene().name);
		yield return new WaitForSeconds(3f);
		NextHoleServerRpc();
	}

	[ServerRpc]
	void NextHoleServerRpc() {
		NetworkSceneManager.SwitchScene(nextScene);
	}

}
