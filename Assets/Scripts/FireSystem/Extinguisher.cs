using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Data;

public class Extinguisher : MonoBehaviourPun
{
    [SerializeField] private LayerMask fireLayer;
    [SerializeField] private float amountToExtinguish;
    [SerializeField] private GameObject fireExtinguisher;
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

                if ((Vector3.Distance(hit.point, transform.position) <= 5) &&fire!=null&&fire.GetComponent<ActivationManager>().onFire )
                {
                    if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Watchmen")
                    {
                        photonView.RPC("ShowTool", RpcTarget.AllViaServer, true);
                        fire.TryExtinguish(amountToExtinguish * Time.deltaTime);
                    }

                        
                }

              
            }
        }
        else
        {
            UIController.instance.slider.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            photonView.RPC("ShowTool", RpcTarget.AllViaServer, false);

        }
    }
    [PunRPC]
    void ShowTool(bool state)

    { fireExtinguisher.SetActive(state); }


}
