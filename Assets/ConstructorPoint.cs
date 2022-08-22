using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ConstructorPoint : MonoBehaviour
{
    public static ConstructorPoint instance;
    private new Camera camera;
    public GameObject building;
    public static Action<Vector3> OnTouchLand;
    private void Awake()
    {
        instance = this;
        camera =Camera.main;
        this.gameObject.SetActive(false);
    }
   public void SetBuilding(GameObject buildingR)
    {
        building= Instantiate(buildingR, new Vector3(0,20,0), Quaternion.identity);
        this.gameObject.SetActive(true);
    }
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit");

                if (hit.collider.gameObject.layer==6)
                {

                    OnTouchLand?.Invoke(hit.point);



                }
                
            }
        }
       
    }
}
