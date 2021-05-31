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


		collider.gameObject.GetComponent<SphereCollider>().enabled = false;
		collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
		collider.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		collider.gameObject.GetComponentInChildren<PlayerCamera>().Finish();

		quantPlayersFinished++;
		if (quantPlayersFinished == quantPlayers) {
			StartCoroutine(ChangeHole());
		} else {
			collider.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
		}
	}

	IEnumerator ChangeHole() {
		yield return new WaitForSeconds(2f);
		NextHoleServerRpc();
	}

	[ServerRpc]
	void NextHoleServerRpc() {
		NetworkSceneManager.SwitchScene(nextScene);
	}

}
