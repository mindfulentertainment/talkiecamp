using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class TouchManagerNetwork : MonoBehaviourPunCallbacks
{

    [Header("Settings for the interaction")]
    [SerializeField] float maxDistance = 3;
    string role;
    GameObject player;
    [SerializeField]  float distance;
    [SerializeField] GameObject saw;
    public bool isGatherer;

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
    private void Awake()
    {
        player = this.gameObject;
        role =this.gameObject.GetComponent<PlayerController>().role;
    }

    void Update()
    {

        
          if (Input.GetMouseButton(0))
          {
                if (photonView.IsMine)
            {

           
                Ray touchRay = GenerateTouchRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(touchRay.origin, touchRay.direction, out hit))
                {
                   
                    Interactable currentInteractable = hit.collider.GetComponent<Interactable>(); // objecto con el que choca el rayo 
                    if (currentInteractable != null)
                    {

                        distance = Vector3.Distance(player.transform.position, currentInteractable.transform.position); //se calcula la distancia del player y el objeto 

                        // se verifica si la distancia es la requerida 
                        if (distance <= maxDistance)
                        {
                            if (LayerMask.LayerToName(hit.collider.gameObject.layer) == role)
                            {
                                hit.collider.GetComponent<Interactable>().OnInteraction();
                                GetComponent<Animator>().SetBool("IsOnInteractable", true);
                                saw.gameObject.SetActive(true);
                            }
                            else
                            {
                                string message = "Solo  el recolector opuede intereactuar con esto, busca su ayuda!";
                                UIController.instance.ShowMessage(message);
                            }

                        }

                    }


                }
            }

            
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isGatherer)
            {
                UIController.instance.slider.gameObject.SetActive(false);
                GetComponent<Animator>().SetBool("IsOnInteractable", false);
                saw.gameObject.SetActive(false);
            }
    

        }
    }

    

}
