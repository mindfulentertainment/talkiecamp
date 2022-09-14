using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPositionManager : MonoBehaviour
{
    [SerializeField] Vector3 inicialPos;
    [SerializeField] GameObject ball;
    [SerializeField] REvents T1Goal, T2Goal, WT1, WT2;
    void Start()
    {
        ball.transform.position = inicialPos;
        T1Goal.GEvent += Reposition;
        T2Goal.GEvent += Reposition;
        WT1.GEvent += EndGame;
        WT2.GEvent += EndGame;
    }

    void Reposition()
    {
        ball.transform.position = inicialPos;
    }
    void EndGame()
    {
        ball.SetActive(false);
    }
    private void OnDestroy()
    {
        T1Goal.GEvent -= Reposition;
        T2Goal.GEvent -= Reposition;
        WT1.GEvent -= EndGame;
        WT2.GEvent -= EndGame;
    }
    private void OnDisable()
    {
        T1Goal.GEvent -= Reposition;
        T2Goal.GEvent -= Reposition;
        WT1.GEvent -= EndGame;
        WT2.GEvent -= EndGame;
    }
    
}
