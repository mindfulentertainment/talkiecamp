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
        }
    }
}
