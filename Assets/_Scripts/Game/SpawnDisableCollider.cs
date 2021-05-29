using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class SpawnDisableCollider : NetworkBehaviour {

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (other.gameObject.GetComponent<PlayerInfo>().GetId() != NetworkManager.Singleton.LocalClientId) {
				other.gameObject.GetComponent<SphereCollider>().isTrigger = true;
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (other.gameObject.GetComponent<PlayerInfo>().GetId() != NetworkManager.Singleton.LocalClientId) {
				other.gameObject.GetComponent<SphereCollider>().isTrigger = false;
			}
		}
	}
}
