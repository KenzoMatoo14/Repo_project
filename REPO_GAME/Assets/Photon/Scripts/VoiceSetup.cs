using Photon.Voice.Unity;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Recorder))]
public class VoiceSetup : MonoBehaviourPun
{
    void Start()
    {
        if (!photonView.IsMine)
        {
            GetComponent<Recorder>().enabled = false;
        }
    }
}
