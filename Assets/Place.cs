using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Place : MonoBehaviour
{
    private void OnEnable()
    {
        var photonV = GetComponent<PhotonView>();
        Destroy(photonV);
    }
}
