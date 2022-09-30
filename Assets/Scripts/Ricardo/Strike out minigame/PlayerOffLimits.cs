using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOffLimits : MonoBehaviour
{
    [SerializeField] REvents playerOut;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            //Debug.Log("Player Out");
            other.GetComponentInParent<PlayerTP>().TPOut();
            playerOut.FireEvent();
        }
    }
}
