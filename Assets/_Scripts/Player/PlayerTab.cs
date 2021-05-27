using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerTab : NetworkBehaviour {

	[SerializeField] GameObject tab;

	List<PlayerInfo> players;
	bool hasEveryoneJoined = false;

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	private void Start() {
		players = new List<PlayerInfo>();
	}



	private void Update() {
		if (IsLocalPlayer) {

			if (!hasEveryoneJoined && SceneManager.GetActiveScene().name == "Hole1") {
				GameObject[] playerGameobject = GameObject.FindGameObjectsWithTag("Player");
				foreach (GameObject player in playerGameobject) {
					players.Add(player.GetComponent<PlayerInfo>());
				}
				hasEveryoneJoined = true;
			}

			if (Input.GetKey(KeyCode.Tab)) {
				if (players.Count != 0) {
					foreach (PlayerInfo player in players) {
						string playerName;
						ulong playerId;
						int playerHits;
						(playerHits, playerId, playerName) = player.GetHits();
						Debug.Log("player: " + playerId + " has " + playerHits + " hits");
					}
					Debug.Log(players.Count);
				} else {
					Debug.Log("0 players");
				}
			}

		}
	}
}
