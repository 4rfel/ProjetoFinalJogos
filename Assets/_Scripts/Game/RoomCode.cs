using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using MLAPI.Transports.PhotonRealtime;

public class RoomCode : NetworkBehaviour {

	Text roomName;
	PhotonRealtimeTransport transport;

	void Start() {
		roomName = GetComponent<Text>();
		transport = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<PhotonRealtimeTransport>();
	}

	private void Update() {
		if (transport == null) {
			transport = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<PhotonRealtimeTransport>();
		} else {
			roomName.text = transport.RoomName;
		}
	}
}
