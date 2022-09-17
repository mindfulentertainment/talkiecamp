using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    [SerializeField] REvents reposition;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            reposition.FireEvent();
            Handheld.Vibrate();
            CameraShake.Shake(0.25f, 0.5f);
        }
    }
}
