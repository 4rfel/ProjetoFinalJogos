using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Water : NetworkBehaviour {

	private void OnTriggerEnter(Collider collision) {
		collision.gameObject.GetComponent<PlayerSoundController>().PlaySound("water");
		collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		collision.gameObject.GetComponent<Rigidbody>().position = collision.gameObject.GetComponent<PlayerMovement>().prePosition;
	}
}
