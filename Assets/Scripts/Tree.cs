using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Unity.Burst.CompilerServices;

public class Tree : Interactable
{

    float timer;
    [SerializeField] float timeOfDestruction;
    [SerializeField] Slider slider;
    [SerializeField] GameObject logs;
    [SerializeField] AudioSource audio1;


    public override void OnInteraction()
    {
        timer = OnDestruction(slider, timeOfDestruction, timer);
        audio1.enabled = true;
        audio1.Play();
        if (timer >= timeOfDestruction)
        {
         
            GetComponent<PhotonView>().RPC("ChangeTree", RpcTarget.All);
        }
    }
    [PunRPC]
    public void ChangeTree()
    {
       
        slider.gameObject.SetActive(false);
        logs.SetActive(true);
        logs.transform.parent = null;
        this.gameObject.SetActive(false);
    }
}
