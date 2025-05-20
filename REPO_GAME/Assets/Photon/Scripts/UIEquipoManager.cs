using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class UIEquipoManager : MonoBehaviour
{
    public GameObject canvasGeneral;
    public GameObject canvasRojo;
    public GameObject canvasAzul;
    public GameObject canvasVerde;

    public void ElegirEquipo(string color)
    {
        PhotonNetwork.Instantiate("PlayerPrefab", new Vector3(0, 0, 0), Quaternion.identity, 0, new object[] { color });

        canvasGeneral.SetActive(false);
        if (color == "Red") canvasRojo.SetActive(true);
        else if (color == "Blue") canvasAzul.SetActive(true);
        else if (color == "Green") canvasVerde.SetActive(true);
    }
}
