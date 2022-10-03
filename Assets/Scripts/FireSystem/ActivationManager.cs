using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationManager : MonoBehaviourPun
{
    [SerializeField] private GameObject FirePs;
    [SerializeField] private float Id;
    private float randomNum;
    public  bool onFire;

    private void Start()
    {
        StartCoroutine(RandomId()); 
    }

    IEnumerator RandomId()
    {
        while(true){
            yield return new WaitForSeconds(5);
            randomNum = Random.Range(1, 6);

            if (randomNum == Id)
            {
                onFire = true;
                photonView.RPC("StarFire",RpcTarget.AllViaServer);
            }
        }
       
    }
    [PunRPC]
    void StarFire()
    {
        FirePs.SetActive(true);
    }

   
 


}
