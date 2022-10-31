using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] Vector2 pointDirect;
    [SerializeField] float time;
    void Start()
    {
        transform.LeanMoveLocal(pointDirect, time).setLoopPingPong();
    }

    
}
