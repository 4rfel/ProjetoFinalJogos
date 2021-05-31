using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using System;

public class PlayerInfo : NetworkBehaviour {

	[SerializeField] NetworkVariable<int> hits = new NetworkVariable<int>(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.OwnerOnly }, 0);
	[SerializeField] NetworkVariable<string> playerName = new NetworkVariable<string>(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.OwnerOnly }, "");
	[SerializeField] NetworkVariable<ulong> playerId = new NetworkVariable<ulong>(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.OwnerOnly }, 0);

	void Start() {
		if (IsLocalPlayer) {
			playerId.Value = NetworkManager.Singleton.LocalClientId;
		}
	}

	internal ulong GetId() {
		return playerId.Value;
	}

	public void AddHit() {
		if (IsLocalPlayer) {
			hits.Value += 1;
		}
	}

	public (int, ulong, string) GetHits() {
		return (hits.Value, playerId.Value, playerName.Value);
	}


	public void ResetScore() {
		hits.Value = 0;
	}


	private void Update() {
		// TODO:
		//		- change nick while on lobby
	}
}
