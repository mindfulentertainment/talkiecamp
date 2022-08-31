using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Tree : Interactable
{
    [SerializeField] GameObject logs;

    [PunRPC]
    public override void OnInteraction()
    {
        logs.SetActive(true);
        logs.transform.parent = null;
        this.gameObject.SetActive(false);
    }
}
