using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationManager : MonoBehaviourPun
{
    [SerializeField] private GameObject FirePs;
    [SerializeField] private float Id;
    FireNetwork fireNetwork;
    private float randomNum;
    public  bool onFire;
    private void Awake()
    {
        fireNetwork = GetComponent<FireNetwork>();
    }
    private void Start()
    {
        StartCoroutine(RandomId()); 
    }

    IEnumerator RandomId()
    {
        while(true){
            yield return new WaitForSeconds(70);
            randomNum = Random.Range(1, 6);

            if (randomNum == Id&&!onFire)
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

        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(StartDamage());

        }
    }

    IEnumerator StartDamage()
    {
        while (fireNetwork.firePs.gameObject.activeSelf)
        {
            UIController.instance.Fire.gameObject.SetActive(true);
            photonView.RPC("Damage", RpcTarget.AllViaServer);
            yield return new WaitForSeconds(5);

        }

        UIController.instance.Fire.gameObject.SetActive(false);
    }

    [PunRPC]
    private void Damage()
    {
        GetComponentInParent<Place>().DamageBuilding(5);
    }


    private void OnDisable()
    {
        UIController.instance.Fire.gameObject.SetActive(false);
        onFire = false;
    }

   
}
