using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class rotate : NetworkBehaviour {
	// Start is called before the first frame update
	void Start() {
		GetComponent<Rigidbody>().AddTorque(transform.right * 200f, ForceMode.Acceleration);
		// transform.Rotate(new Vector3(0f,0f,1f));
	}
}
