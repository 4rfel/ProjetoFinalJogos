using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MLAPI;

public class ChangeName : NetworkBehaviour {

	[SerializeField] InputField playerName;

	PlayerInfo playerInfo;

	void Start() {
		if (IsLocalPlayer) {
			playerInfo = GetComponent<PlayerInfo>();
		}
	}

	void Update() {
		if (IsLocalPlayer) {
			playerInfo.setName(playerName.text);

			if (SceneManager.GetActiveScene().name != "Lobby" && SceneManager.GetActiveScene().name != "Menu") {
				Destroy(playerName.gameObject);
				Destroy(this);
			}
		}
	}
}
