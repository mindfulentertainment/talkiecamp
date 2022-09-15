using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPositionManager : MonoBehaviour
{
    [SerializeField] Transform inicialPos;
    [SerializeField] GameObject ball;
    [SerializeField] REvents T1Goal, T2Goal, WT1, WT2;
    void Start()
    {
        Reposition();
        T1Goal.GEvent += Reposition;
        T2Goal.GEvent += Reposition;
        WT1.GEvent += EndGame;
        WT2.GEvent += EndGame;
    }

    void Reposition()
    {
        ball.transform.position = inicialPos.position;
        ball.transform.parent = null;
    }
    void EndGame()
    {
        //ball.SetActive(false);
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
