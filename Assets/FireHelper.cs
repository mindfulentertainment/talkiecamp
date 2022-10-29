using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class FireHelper : MonoBehaviourPun
{
    public float RepairAmount = 7;
    ActivationManager fire = null;
    Coroutine coroutine = null;
    PlayerController characterController = null;
    public GameObject fireExtinguisher;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Placed"))
        {
            if (photonView.IsMine)
            {
                fire = other.transform.parent.GetComponentInChildren<ActivationManager>();
                if (fire == null) return;
                if (fire.onFire)
                {
                    UIController.instance.fireHelper.SetActive(true);


                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Placed"))
        {
            fireExtinguisher.SetActive(false);

            UIController.instance.fireHelper.SetActive(false);
        }
    }

    


 
}
