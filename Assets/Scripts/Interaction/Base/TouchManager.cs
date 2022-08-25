using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{

    [Header("Settings for the interaction")]
    [SerializeField] float maxDistance = 3;
    [SerializeField] LayerMask InteractionLayer;
    [SerializeField] GameObject playerPos;

    [SerializeField]  float distance;

    private Interactable currentInteractable;
    Ray GenerateTouchRay(Vector3 touchPos)
    {
        // calculamos  el  ray  desde el punto mas cercano a la camara hasta el mas lejano 
        Vector3 touchposFar = new Vector3(touchPos.x, touchPos.y, Camera.main.farClipPlane);
        Vector3 touchposNear = new Vector3(touchPos.x, touchPos.y, Camera.main.nearClipPlane);
      
        // hacemos el cambio de la posicion de screen(2d) a world(3d) 
        Vector3 touchposF = Camera.main.ScreenToWorldPoint(touchposFar);
        Vector3 touchposN = Camera.main.ScreenToWorldPoint(touchposNear);

        // generamos el ray desde la posicion mas cercana a la camara y calculamos su direccion en base a las dos posciones 
        Ray touchRay = new Ray(touchposN, touchposF - touchposN);
        Debug.DrawRay(touchposN, touchposF - touchposN, Color.green);
        return touchRay;
    }

    
    void Update()
    {

        //if (Input.touchCount > 0) GetTouch(0).phase == TouchPhase.Began
        //{
            if (Input.GetMouseButton(0))
            {
                Ray touchRay = GenerateTouchRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(touchRay.origin, touchRay.direction, out hit, InteractionLayer)){
                     currentInteractable = hit.collider.GetComponent<Interactable>(); // objecto con el que choca el rayo 
                if (currentInteractable != null)
                {
                    distance = Vector3.Distance(playerPos.transform.position, currentInteractable.transform.position); //se calcula la distancia del player y el objeto 

                    // se verifica si la distancia es la requerida 
                    if (distance <= maxDistance)
                        currentInteractable.OnInteraction();
                    else Debug.Log("out of range");

                }
                }
            }
       // }
    }

}
