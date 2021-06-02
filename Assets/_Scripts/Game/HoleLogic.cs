using System.Collections;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.SceneManagement;

public class HoleLogic : NetworkBehaviour {

	[SerializeField] string nextScene;

	private void Start() {
		StartCoroutine(ChangeHole());
	}

	private void OnTriggerEnter(Collider collider) {
		collider.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
		collider.gameObject.GetComponent<SphereCollider>().enabled = false;
		collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
		collider.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		collider.gameObject.GetComponentInChildren<PlayerCamera>().Finish();
	}

	IEnumerator ChangeHole() {
		while (true) {
			int quantPlayers = NetworkManager.Singleton.ConnectedClientsList.Count;

			yield return new WaitForSeconds(2f);
			int quantPlayersFinished = 0;
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject player in players) {
				if (player.GetComponent<PlayerCamera>().finished.Value)
					quantPlayersFinished++;
			}
			if (quantPlayersFinished == quantPlayers) {
				NextHoleServerRpc();
			}
		}
	}

	[ServerRpc]
	void NextHoleServerRpc() {
		NetworkSceneManager.SwitchScene(nextScene);
	}
}
