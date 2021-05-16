using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using MLAPI.SceneManagement;
using MLAPI.Transports.PhotonRealtime;

public class MenuButtons : NetworkBehaviour {

	[SerializeField] GameObject buttons;
	[SerializeField] InputField roomName;
	[SerializeField] Text log;
	PhotonRealtimeTransport transport;

	private void Start() {
		transport = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<PhotonRealtimeTransport>();
	}

	public void SetRoomName() {
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
		if (transport.RoomName.Length != 6) {
			log.gameObject.SetActive(true);
			log.text = "RoomCode need to have 6 chars";
			return;
		}
		//Camera.main.gameObject.SetActive(false);
		NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("v0");
		NetworkManager.Singleton.OnClientDisconnectCallback += OnDiconnectClient();
		NetworkManager.Singleton.StartClient();
		buttons.SetActive(false);
	}

	private System.Action<ulong> OnDiconnectClient() {
		log.gameObject.SetActive(true);
		log.text = "failed to join room: " + transport.RoomName;
		buttons.SetActive(true);

		return aa ;
	}

	private void aa(ulong obj) {
		return;
	}

	public void Quit() {
		Application.Quit();
	}
}
