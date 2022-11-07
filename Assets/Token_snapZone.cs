using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Token_snapZone : MonoBehaviourPun
{
    public int key;

    public bool isAvailable = true;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient && GetComponent<Ingredient>() == null)
        {
            key = this.gameObject.GetHashCode();
            photonView.RPC("SetKey", RpcTarget.AllViaServer, key);
        }
    }

    private void Start()
    {
        if (GetComponent<Ingredient>() != null)
        {
            CreateToken();
        }
    }

    [PunRPC]
    public void SetKey(int k)
    {
        key = k;
        CreateToken();
    }


    public virtual void CreateToken()
    {
        SnapZone pickable = GetComponent<SnapZone>();
        Token_Manager.DefaultInstance.snapzones_tokens.Add(key, pickable);
    }

}
