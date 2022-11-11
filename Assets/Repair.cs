using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Unity.VisualScripting;

public class Repair : MonoBehaviourPun
{
    public float RepairAmount = 7;
    Place place=null;
    Coroutine coroutine=null;
    public Sprite hammerSprite;
    PlayerController characterController=null;
    public GameObject hammer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Placed"))
        {
            place = other.GetComponentInParent<Place>();
            if(place ==null|| place.buildingHistory == null) return;
            
            if (place.buildingHistory.health < place.maxHealth)
            {
                if (UIController.instance == null) return;
                if (photonView.IsMine)
                {
                    UIController.instance.SwitchPickSprite(hammerSprite);

                }
                UIController.instance.repairHelper.SetActive(true);
                UIController.instance.pickBtn.GetComponent<Button>().onClick.AddListener(RepairBuilding);
               

            }
        }
    }
    private void Awake()
    {
        characterController=GetComponentInParent<PlayerController>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Placed"))
        {
            hammer.SetActive(false);
            if (photonView.IsMine)
            {
                UIController.instance.ResetPickSprite();


            }
            UIController.instance.repairHelper.SetActive(false);
            UIController.instance.pickBtn.GetComponent<Button>().onClick.RemoveListener(RepairBuilding);
        }
    }

    public void RepairBuilding()
    {
        if (GetComponentInParent<PhotonView>().IsMine)
        {
            if (!characterController.isRunning)
            {
                if (place != null && coroutine == null)
                {
                    Vector3 target = place.gameObject.transform.position;
                    coroutine = StartCoroutine(RepairBuildingCoroutine());
                    string t = target.ToString();
                    photonView.RPC("SendRepair",RpcTarget.AllBufferedViaServer, t);


                }
            }
        }
      

      
    }

    [PunRPC]
    void SendRepair(string target)
    {
        hammer.SetActive(true);
        Place place =null ;
        DataManager.instance.buildingsDictionary[target]?.gameObject.TryGetComponent(out place);
        if(place != null)
        {
            if (photonView.IsMine)
            {
                UIController.instance.SwitchPickSprite(hammerSprite);

            }

            place.RepairBuilding(RepairAmount);
        }
        GetComponentInParent<Animator>().SetTrigger("isRepearing");

    }
    IEnumerator RepairBuildingCoroutine()
    {

        yield return new WaitForSeconds(1.2f); 
        coroutine = null;
       
    }


}
