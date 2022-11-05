using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class GameManager : MonoBehaviourPun
{
    public GameObject tutorial;
    private void Start()
    {
        StateManager.Instance.OnResourcesLoad.AddListener(ReceiveData);
    }



    private void OnDisable()
    {
        StateManager.Instance.OnResourcesLoad.RemoveListener(ReceiveData);

    }
    private void ReceiveData(Resource resource, Buildings buildings)
    {
        if (!resource.newCamp)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("ShowTutorial", RpcTarget.AllViaServer);
            }
        }
    }
    [PunRPC]
    public void ShowTutorial()
    {
        tutorial.SetActive(true);

    }

}
