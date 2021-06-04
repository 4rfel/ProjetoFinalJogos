using System.Collections.Generic;
using MLAPI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerTab : NetworkBehaviour {

	[SerializeField] GameObject tab;
	SpawnTabLine spawnTabLine;

	List<PlayerInfo> players;
	bool hasEveryoneJoined = false;

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	private void Start() {
		players = new List<PlayerInfo>();
		spawnTabLine = tab.GetComponent<SpawnTabLine>();
	}

	private void Update() {
		if (IsLocalPlayer) {

			if (!hasEveryoneJoined && (SceneManager.GetActiveScene().name == "Hole1" || SceneManager.GetActiveScene().name == "Hole2"|| SceneManager.GetActiveScene().name == "Hole3")) {
				GameObject[] playerGameobject = GameObject.FindGameObjectsWithTag("Player");
				foreach (GameObject player in playerGameobject) {
					players.Add(player.GetComponent<PlayerInfo>());
				}
				hasEveryoneJoined = true;
			}

			if (Input.GetKeyDown(KeyCode.Tab)) {
				tab.SetActive(true);
				spawnTabLine.Clear();
				if (players.Count != 0) {
					foreach (PlayerInfo player in players) {
						string playerName;
						ulong playerId;
						int playerHits;
						(playerHits, playerId, playerName) = player.GetHits();
						spawnTabLine.SpawnLine(playerHits, playerId.ToString());
					}
				}
			} else if (Input.GetKeyUp(KeyCode.Tab)) {

				tab.SetActive(false);
			}
		}
	}
}
