using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveArrow : MonoBehaviour
{
    [SerializeField] Vector2 pointDirect;
    [SerializeField] float time;
    void Start()
    {
        transform.LeanMoveLocal(pointDirect, time).setEaseOutQuart().setLoopPingPong();
    }

    
}
