using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.SceneManagement;

public class HoleLogic : NetworkBehaviour {

	int quantPlayersFinished = 0;

	[SerializeField] string nextScene;

	private void OnTriggerEnter(Collider collider) {
		int quantPlayers = NetworkManager.Singleton.ConnectedClientsList.Count;


		collider.gameObject.GetComponent<SphereCollider>().enabled = false;
		collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
		collider.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		collider.gameObject.GetComponentInChildren<PlayerCamera>().Finish();
		quantPlayersFinished++;
		Debug.Log(quantPlayersFinished + " " + quantPlayers);
		if (quantPlayersFinished == quantPlayers) {
			StartCoroutine(ChangeHole());
		} else {
			collider.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
		}
	}

	IEnumerator ChangeHole() {
		quantPlayersFinished = 0;
		yield return new WaitForSeconds(2f);
		NextHoleServerRpc();
	}

	[ServerRpc]
	void NextHoleServerRpc() {
		NetworkSceneManager.SwitchScene(nextScene);
	}

}
