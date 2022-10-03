using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BearManager : MonoBehaviourPun
{
    public static BearManager instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject bear;
    private Coroutine m_Coroutine;
    public Transform restPos;



    private void OnEnable()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            m_Coroutine = StartCoroutine(ActivateBearC());
        }
    }

    private void OnDisable()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if(m_Coroutine != null)
            {
                StopCoroutine(m_Coroutine);
            }
            
        }
    }


    IEnumerator ActivateBearC()
    {
        yield return new WaitForSeconds(10);
        photonView.RPC("ActivateBear", RpcTarget.AllViaServer);

    }


    [PunRPC]
    public void ActivateBear()
    {
        bear.SetActive(true);

    }
}
