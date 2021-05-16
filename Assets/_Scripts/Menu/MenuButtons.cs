﻿using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using MLAPI.SceneManagement;
using MLAPI.Spawning;


public class MenuButtons : NetworkBehaviour {

	[SerializeField] GameObject buttons;
	[SerializeField] InputField roomName;
	//[SerializeField] GameObject loading;
	MLAPI.Transports.PhotonRealtime.PhotonRealtimeTransport transport;

	private void Start() {
		transport = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<MLAPI.Transports.PhotonRealtime.PhotonRealtimeTransport>();
	}

	void Update() {
		if (roomName.text.Length == 6) {
			transport.RoomName = roomName.text.ToUpper();
		}
	}

	public void CreateLobby() {
		string str = "";
		for (int i = 0; i < 6; i++) {
			str += (char) Random.Range(65, 90);
		}
		transport.RoomName = str;
		Camera.main.gameObject.SetActive(false);
		NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
		NetworkManager.Singleton.StartHost();
		NetworkSceneManager.SwitchScene("Lobby");
	}

	private void ApprovalCheck(byte[] connectionData, ulong clientId, NetworkManager.ConnectionApprovedDelegate callback) {
		bool approve = System.Text.Encoding.ASCII.GetString(connectionData) == "v0";
		
		callback(true, null, approve, Vector3.zero, Quaternion.identity);
	}

	public void Join() {
		if (transport.RoomName.Length != 6)
			return;
		//Camera.main.gameObject.SetActive(false);
		NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("v0");
		NetworkManager.Singleton.StartClient();
		buttons.SetActive(false);
	}

	public void Quit() {
		Application.Quit();
	}

	//public void OnJoinRoomFailed() {
		
	//}

}
