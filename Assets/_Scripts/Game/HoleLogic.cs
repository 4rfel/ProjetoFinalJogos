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
		Debug.Log(quantPlayers);
	}

	private void OnTriggerEnter(Collider collider) {
		collider.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		quantPlayersFinished++;
		if (quantPlayersFinished == quantPlayers) {
			StartCoroutine(ChangeHole());
		}
	}

	IEnumerator ChangeHole() {
		yield return new WaitForSeconds(3f);
		NextHoleServerRpc();
	}

	[ServerRpc]
	void NextHoleServerRpc() {
		NetworkSceneManager.SwitchScene(nextScene);
	}

}
