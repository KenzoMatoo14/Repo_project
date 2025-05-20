using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // Se conecta a los servidores de Photon
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado a Photon.");
        PhotonNetwork.JoinLobby(); // Entrar a un lobby general
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("En lobby. Creando o uniéndose a sala...");
        PhotonNetwork.JoinOrCreateRoom("SalaTest", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("¡Entraste a la sala!");

        // Spawnea jugador
        PhotonNetwork.Instantiate("REPO Animation Sketchfab", new Vector3(-10.50564f, 0.0f, 43.7599983f), Quaternion.identity);
    }
}
