using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MakeBallBigger : Interactable
{
    [PunRPC]
    public override void OnInteraction()
    {
        //Material material = GetComponent<Material>();
        //material.SetColor("_Color", Color.blue);
        transform.localScale = new Vector3(3,3,3);
        Debug.Log("override");
    }
}
