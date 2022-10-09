using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieveObject : MonoBehaviour
{
    [SerializeField] REvents call;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("object"))
        {
            call.FireEvent();
            other.GetComponent<ShootObject>().Disappear();
        }
    }
}
