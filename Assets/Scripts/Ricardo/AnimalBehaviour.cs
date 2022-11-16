using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class AnimalBehaviour : MonoBehaviourPun
{
    [SerializeField] float speed,waitTime,startWaitTime,angle; //startWaitTime es el que se le debe cambiar la variable en el inspector
    [SerializeField] Transform[] moveSpots;
    [SerializeField] int randomSpot;
    [SerializeField] bool trapped;
    [SerializeField] Vector3 dif;

    [SerializeField] GameObject meat;

    void OnEnable()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            waitTime = startWaitTime;
            randomSpot = Random.Range(0, moveSpots.Length);
        }
       
    }
    
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (trapped == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
                transform.LookAt(moveSpots[randomSpot]);

                if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
                {
                    if (waitTime <= 0)
                    {
                        randomSpot = Random.Range(0, moveSpots.Length);
                        transform.LookAt(moveSpots[randomSpot]);

                        waitTime = startWaitTime;
                    }
                    else
                    {
                        waitTime -= Time.deltaTime;
                    }
                }
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (other.CompareTag("Trap"))
            {
                trapped = true;
                transform.position = other.transform.position;
                photonView.RPC("KillAnimal", RpcTarget.AllViaServer);
            }

        }
    
    }
    private void OnTriggerExit(Collider other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (other.CompareTag("Trap"))
            {
                trapped = false;
            }

        }
        
    }

    [PunRPC]
    void KillAnimal()
    {
       
        meat.SetActive(true);
        meat.transform.parent = null;

        this.gameObject.SetActive(false);
    }
}
