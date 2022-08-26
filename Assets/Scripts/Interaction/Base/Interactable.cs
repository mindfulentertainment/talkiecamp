using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Interactable : MonoBehaviourPunCallbacks
{
    [PunRPC]
    public virtual void OnInteraction()
    {
        Debug.Log("Interaction");
    }
}
