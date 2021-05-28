using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent < Rigidbody >().AddTorque(transform.right * 100f,ForceMode.Acceleration);
        // transform.Rotate(new Vector3(0f,0f,1f));
    }

    
}
