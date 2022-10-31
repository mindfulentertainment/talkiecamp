using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUIBall : MonoBehaviour
{
    [SerializeField] Vector2 start,pointDirect;
    [SerializeField] float time;
    void Start()
    {
        start = transform.localPosition;
        transform.LeanMoveLocal(pointDirect, time).setOnComplete(Shrink);
        
    }
    void Shrink()
    {
        transform.localScale = Vector3.zero;
        transform.localPosition = start;
        transform.localScale = Vector3.one;
        transform.LeanMoveLocal(pointDirect, time).setOnComplete(Shrink);
    }
}
