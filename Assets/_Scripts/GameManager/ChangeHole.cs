using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;

public class ChangeHole : NetworkBehaviour {

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	void Update() {

	}
}
