using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootBallNet : MonoBehaviour
{
    [SerializeField] REvents teamGoal,restartBall; 
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            teamGoal.FireEvent();
            restartBall.FireEvent();
            Handheld.Vibrate();
            CameraShake.Shake(0.25f, 0.5f);
        }
    }
}
