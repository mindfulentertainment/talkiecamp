using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RecogePelotas : MonoBehaviourPun
{
    [SerializeField] REvents gEvent;
    [SerializeField] REvents startGathering;
    public void FireButton()
    {
        photonView.RPC("StartGame", RpcTarget.AllViaServer);
    }

    [PunRPC]
    void StartGame()
    {
        gEvent.FireEvent();
    }
}
