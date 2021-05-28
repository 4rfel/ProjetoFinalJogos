using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Water : NetworkBehaviour
{
    

    private void OnCollisionEnter(Collision collision) {
		//PlaySound("hit");
		collision.gameObject.GetComponent<Rigidbody>().position= collision.gameObject.GetComponent<PlayerMovement>().prePosition;

    }
}
