using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    private void OnEnable()
    {
        ConstructorPoint.OnTouchLand += UpdateTransform;
    }
    private void OnDisable()
    {
        ConstructorPoint.OnTouchLand -= UpdateTransform;

    }

    private void UpdateTransform(Vector3 pos)
    {
        gameObject.transform.transform.position = pos;
    }
}
