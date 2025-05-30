using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameManagerTRegina : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameManagerTRegina Instance;

    public bool bool1 = false;
    public bool bool2 = false;

    void Awake()
    {
        // Solo una instancia global
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(bool1);
            stream.SendNext(bool2);
        }
        else
        {
            bool1 = (bool)stream.ReceiveNext();
            bool2 = (bool)stream.ReceiveNext();
        }
    }
    void Update()
    {
        if (bool1 && bool2)
        {
            Debug.Log("¡Cuarto terminó sus misiones!");
            // Aquí puedes poner cualquier lógica adicional cuando ganen las misiones
        }
    }
    // Métodos para modificar los bools, accesibles desde otros scripts
    public void CompletarPrimerMision()
    {
        bool1 = true;
    }

    public void CompletarSegundaMision()
    {
        bool2 = true;
    }
}