using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : MonoBehaviourPun
{
    public float speed = 5f;
    public Renderer bodyRenderer;

    void Start()
    {
        if (photonView.IsMine)
        {
            Camera mainCam = Camera.main;
            mainCam.transform.SetParent(transform);

            // Posición para tercera persona
            mainCam.transform.localPosition = new Vector3(0, 1.5f, -3f); // ajusta a gusto
            mainCam.transform.localRotation = Quaternion.Euler(10f, 0f, 0f);
        }
        // Leer color si viene en los datos de instanciación
        object[] data = photonView.InstantiationData;
        if (data != null && data.Length > 0)
        {
            string colorName = (string)data[0];
            Color color = Color.white;

            switch (colorName)
            {
                case "Red": color = Color.red; break;
                case "Blue": color = Color.blue; break;
                case "Green": color = Color.green; break;
            }

            // Crear instancia del material para no compartirlo
            if (bodyRenderer != null)
                bodyRenderer.material = new Material(bodyRenderer.material);

            bodyRenderer.material.color = color;

            // Guardar la propiedad solo si este objeto es del jugador local
            if (photonView.IsMine)
            {
                Hashtable props = new Hashtable { { "equipo", colorName } };
                PhotonNetwork.LocalPlayer.SetCustomProperties(props);
            }
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        transform.Translate(dir * speed * Time.deltaTime, Space.World);
    }
}
