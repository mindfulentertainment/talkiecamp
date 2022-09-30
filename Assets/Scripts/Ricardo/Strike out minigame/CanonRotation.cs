using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRotation : MonoBehaviour
{
    [SerializeField]float angle,angularSpeed,amplitud;
    
    [SerializeField] Vector3 rotation;

    void Start()
    {
        angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime*angularSpeed;
        rotation.y = amplitud * Mathf.Sin(angle)+90;

        transform.eulerAngles = rotation;
        if (angle > 2 * Mathf.PI)
        {
            angle = 0;
        }
    }
}
