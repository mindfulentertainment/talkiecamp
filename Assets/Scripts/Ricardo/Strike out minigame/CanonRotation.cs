using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CanonRotation : MonoBehaviour
{
    [SerializeField]float angle,angularSpeed,amplitud;

    Vector3 dir;
    [SerializeField] Vector3 rotation;

    public bool opposite; 

    void Start()
    {
        
        angle = 0;
       
    }
   
    // Update is called once per frame
    void Update()
    {
      
            angle += Time.deltaTime * angularSpeed;
            if (opposite) 
            {
                rotation.y = amplitud * Mathf.Sin(angle) + 90;

            }
            else
            {
                rotation.y = amplitud * Mathf.Sin(-angle) + 90;

            }
            dir.x = transform.eulerAngles.x;
            dir.z = transform.eulerAngles.z;
            dir.y = rotation.y;
            transform.eulerAngles = dir;
            if (angle > 2 * Mathf.PI)
            {
                angle = 0;
            }
        
        
    }
}
