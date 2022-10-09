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
    public Color danger;
    public Color calm;


    private void OnEnable()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            m_Coroutine = StartCoroutine(ActivateBearC(Random.Range(20,30)));
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
    public void ReactivateBear()
    {
        if(m_Coroutine != null)
        {
            StopCoroutine(m_Coroutine);
        }
       
        m_Coroutine = StartCoroutine(ActivateBearC(Random.Range(10, 13)));
       
    }

    IEnumerator ActivateBearC(float t)
    {
        Debug.Log($"Reactivating in {t}");
        yield return new WaitForSeconds(t);
        bool buildings= false;

        while (!buildings)
        {
            if (DataManager.instance.buildings.buildings.Count > 1)
            {
                photonView.RPC("ActivateBear", RpcTarget.AllViaServer);

                    buildings=true;

            }
            else
            {
                Debug.Log("No buildings");
            }


            yield return new WaitForSeconds(10);
        }


    }


    [PunRPC]
    public void ActivateBear()
    {
        
        bear.SetActive(true);
        CameraControllerNetWork.instance.ChangeTarget(bear.gameObject.transform);
        CameraControllerNetWork.instance.gameObject.GetComponent<EditValues>().VignetteAmount = 0.12f;
        CameraControllerNetWork.instance.gameObject.GetComponent<EditValues>().VignetteColor = danger;
        UIController.instance.bearNear.SetActive(true);
        StartCoroutine(CenterPlayer());
    }

    IEnumerator CenterPlayer()
    {
        yield return new WaitForSeconds(8);
        CameraControllerNetWork.instance.CenterPlayer();
        CameraControllerNetWork.instance.gameObject.GetComponent<EditValues>().VignetteAmount = 0.058f;

        CameraControllerNetWork.instance.gameObject.GetComponent<EditValues>().VignetteColor = calm;
        UIController.instance.bearNear.SetActive(false);



    }



}
