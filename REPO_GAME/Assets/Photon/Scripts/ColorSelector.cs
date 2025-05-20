using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class ColorSelector : MonoBehaviour
{
    public void SetColorRed() { PlayerCustomizationData.selectedColor = "Red"; LoadGame(); }
    public void SetColorBlue() { PlayerCustomizationData.selectedColor = "Blue"; LoadGame(); }
    public void SetColorGreen() { PlayerCustomizationData.selectedColor = "Green"; LoadGame(); }

    public void LoadGame()
    {
        PhotonNetwork.LoadLevel("escenaprincipalConPhoton");
    }
}

