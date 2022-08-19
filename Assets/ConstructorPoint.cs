using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ConstructorPoint : MonoBehaviour
{
    private new Camera camera;
    public static Action<Vector3> OnTouchLand;
    private void Awake()
    {
        camera=GetComponent<Camera>();
    }
   
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer==6)
                {
                   
                        OnTouchLand?.Invoke(hit.point);

                    
                }
                
            }
        }
       
    }
}
