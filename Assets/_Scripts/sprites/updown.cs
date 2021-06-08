using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class updown : NetworkBehaviour {

	Vector3 angularVelocity;
	Rigidbody rb;
    private float time = 0.0f;
    private int up=0;

	void Start() {
		rb = GetComponent<Rigidbody>();
		
        rb.velocity = new Vector3(0, 10, 0);
        up=1;
		// angularVelocity = rb.angularVelocity;
	}

	private void Update() {
		time = time + Time.fixedDeltaTime;
        if (time > 10.0f)
        {
            if( up == 1){
                rb.velocity = new Vector3(0, -10, 0);
                up =0;
            }
            else{

                rb.velocity = new Vector3(0, 10, 0);
                up =1;

            }
            
            time = 0.0f;
        }
	}
}
