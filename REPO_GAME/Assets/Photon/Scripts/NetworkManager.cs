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

        // Cargar datos de personalización
        object[] instantiationData = new object[] { PlayerCustomizationData.selectedColor };

        // Instanciar al jugador con los datos
        PhotonNetwork.Instantiate("REPO Animation Sketchfab", new Vector3(-10.50564f, 0.0f, 43.7599983f), Quaternion.identity, 0, new object[] { });
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.InstantiateRoomObject("instanceKenzao", Vector3.zero, Quaternion.identity);
            PhotonNetwork.InstantiateRoomObject("instanceRegina", Vector3.zero, Quaternion.identity);
            PhotonNetwork.InstantiateRoomObject("instanceSoto", Vector3.zero, Quaternion.identity);
            PhotonNetwork.InstantiateRoomObject("instanceZatarain", Vector3.zero, Quaternion.identity);
            PhotonNetwork.InstantiateRoomObject("intanceRo", Vector3.zero, Quaternion.identity);

            // Y así con todas
        }
    }
}
