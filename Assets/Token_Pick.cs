using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Token_Pick : MonoBehaviourPun
{
    public int key;
 
    public bool isAvailable=true;

    private void Awake()
    {
        if(PhotonNetwork.IsMasterClient&&GetComponent<Ingredient>()==null&& GetComponentInParent<PlantGrowth>() == null)
        {
            key = this.gameObject.GetHashCode();
            photonView.RPC("SetKey", RpcTarget.AllViaServer, key);
        }
       
        
        
    }
    private void Start()
    {
        if ( GetComponent<Ingredient>() != null)
        {
            CreateToken();
        }

        if (GetComponentInParent<PlantGrowth>() != null)
        {
            CreateToken();
        }
    }

    [PunRPC]
    public void SetKey(int k)
    {
        IPickable pickable = GetComponent<IPickable>();
        key = k;
        Token_Manager.DefaultInstance.pickables_tokens.Add(key, pickable);
    }

    
    public virtual void CreateToken()
    {
        IPickable pickable = GetComponent<IPickable>();
        key = Token_Manager.DefaultInstance.pickables_tokens.Count + 1;
        Token_Manager.DefaultInstance.pickables_tokens.Add(key, pickable);
    }
}
