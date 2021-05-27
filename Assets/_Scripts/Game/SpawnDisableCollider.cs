using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class SpawnDisableCollider : NetworkBehaviour {

	private void OnTriggerEnter(Collider other) {
		Debug.Log(other.gameObject.tag);
		if (other.gameObject.tag == "Player") {
			Debug.Log(other.gameObject.GetComponent<PlayerInfo>().GetId() + " enter " + NetworkManager.Singleton.LocalClientId);
			if (other.gameObject.GetComponent<PlayerInfo>().GetId() != NetworkManager.Singleton.LocalClientId) {
				other.gameObject.GetComponent<SphereCollider>().isTrigger = true;
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		Debug.Log(other.gameObject.tag);
		if (other.gameObject.tag == "Player") {
			Debug.Log(other.gameObject.GetComponent<PlayerInfo>().GetId() + " exit " + NetworkManager.Singleton.LocalClientId);
			if (other.gameObject.GetComponent<PlayerInfo>().GetId() != NetworkManager.Singleton.LocalClientId) {
				other.gameObject.GetComponent<SphereCollider>().isTrigger = false;
			}
		}
	}
}
