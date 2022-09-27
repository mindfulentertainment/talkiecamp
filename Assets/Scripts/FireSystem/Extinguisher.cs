using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    [SerializeField] private LayerMask fireLayer;
    [SerializeField] private float amountToExtinguish;
   
    private FireNetwork fire;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray touchRay = TouchManager.GenerateTouchRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(touchRay.origin, touchRay.direction, out hit, 100, fireLayer))
            {
                fire = hit.collider.GetComponent<FireNetwork>();
                fire.TryExtinguish(amountToExtinguish * Time.deltaTime);
               
            }
        }
    }
}
