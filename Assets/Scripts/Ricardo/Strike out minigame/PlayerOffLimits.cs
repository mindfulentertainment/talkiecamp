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
            var tp = other.GetComponentInParent<PlayerTP>();
            if (tp != null)
            {
                tp.TPOut();
            }
            playerOut.FireEvent();
        }
    }
}
