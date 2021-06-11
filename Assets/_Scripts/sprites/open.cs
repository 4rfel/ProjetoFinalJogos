using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class open : NetworkBehaviour {

	Vector3 angularVelocity;
	Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
		rb.angularVelocity = Vector3.forward * 0.5f;
		angularVelocity = rb.angularVelocity;
	}

	private void Update() {
		if (rb.angularVelocity.magnitude != angularVelocity.magnitude) {
			rb.angularVelocity = angularVelocity;
		}
	}
}
