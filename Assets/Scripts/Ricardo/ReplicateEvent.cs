using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ReplicateEvent : MonoBehaviourPunCallbacks
{
    [SerializeField] REvents rEvents;

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        rEvents.FireEvent();
    //    }
    //}
    private void Start()
    {
        UIController.instance.pickBtn.onClick.AddListener(ReadInput);
    }
    private void OnDisable()
    {
        UIController.instance.pickBtn.onClick.RemoveListener(ReadInput);

    }
    
    [PunRPC]
    public void LaunchBall()
    {
        
            rEvents.FireEvent();
       
        

    }
    void ReadInput()
    {
        var ball = PlayerSpawner.GetMyPlayer()?.GetComponentInChildren<OnPlayerFoot>();
        if (ball != null)
        {
        photonView.RPC("LaunchBall", RpcTarget.AllViaServer);
        }
    }

}
