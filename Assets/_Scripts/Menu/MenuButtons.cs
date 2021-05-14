using UnityEngine;
using MLAPI;
using MLAPI.SceneManagement;

public class MenuButtons : NetworkBehaviour {

	[SerializeField] GameObject buttons;
	//[SerializeField] GameObject loading;
	MLAPI.Transports.PhotonRealtime.PhotonRealtimeTransport transport;


	private void Start() {
		//transport = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<MLAPI.Transports.PhotonRealtime.PhotonRealtimeTransport>();
	}

	public void Host() {
		//string str = "";
		//for(int i = 0; i < 6; i++) {
		//	str += (char) Random.Range(65, 90);
		//}
		//transport.RoomName = str;
		NetworkManager.Singleton.StartHost();
		NetworkSceneManager.SwitchScene("Loby");
	}

	public void Join() {
		//if (transport.RoomName.Length != 6)
		//	return;
		NetworkManager.Singleton.StartClient();
		buttons.SetActive(false);
	}

	public void Quit() {
		Application.Quit();
	}
}
