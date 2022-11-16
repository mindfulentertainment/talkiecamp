using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Data;

public class Extinguisher : MonoBehaviourPun
{
    [SerializeField] private float amountToExtinguish;
    [SerializeField] private GameObject fireExtinguisher;
    private FireNetwork fire;
    string myRole;

  
    private void Start()
    {
        myRole =this.gameObject.GetComponent<PlayerController>().role;

    }
    void Update()
    {
        
            if (Input.GetMouseButton(0))
            {
                


                Ray touchRay = TouchManager.GenerateTouchRay(Input.mousePosition);
                RaycastHit hit;


                if (Physics.Raycast(touchRay.origin, touchRay.direction, out hit, 100))
                {
                    if (photonView.IsMine)
                    {
                        if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Watchmen")
                        {

                                var burn = hit.collider.GetComponentInParent<Burn>();

                        if (burn!= null){
                            fire = burn.GetComponentInChildren<FireNetwork>();
                        }

                            if ((Vector3.Distance(hit.point, transform.position) <= 5) && fire != null && fire.GetComponent<ActivationManager>().onFire)
                            {

                        

                                    photonView.RPC("ShowTool", RpcTarget.AllViaServer, true);
                                    fire.TryExtinguish(amountToExtinguish * Time.deltaTime);
                                fire = null;


                            }
                        }
                    }
                  


                }
            }
            else
            {
                if (photonView.IsMine)
                {
                    UIController.instance.slider.gameObject.SetActive(false);

                }
        }

            if (Input.GetMouseButtonUp(0))
            {
                if (photonView.IsMine)
                {
                    photonView.RPC("ShowTool", RpcTarget.AllViaServer, false);


                }

        }
        
        
    }
    [PunRPC]
    void ShowTool(bool state)

    { fireExtinguisher.SetActive(state); }


}
