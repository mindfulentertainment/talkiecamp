using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootBallNet : MonoBehaviour
{
    [SerializeField] REvents teamGoal,restartBall;
    [SerializeField] bool net;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (net == true)
            {
                teamGoal.FireEvent();
                restartBall.FireEvent();
                Handheld.Vibrate();
                CameraShake.Shake(0.25f, 0.5f);
            }
            else
            {
                restartBall.FireEvent();
            }
        }
    }
}
