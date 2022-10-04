using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Repair : MonoBehaviourPun
{
    public float RepairAmount = 7;
    Place place=null;
    Coroutine coroutine=null;
    PlayerController characterController=null;
    public GameObject hammer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Placed"))
        {
            place = other.GetComponentInParent<Place>();
            if (place.health < place.maxHealth)
            {
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
        DataManager.instance.buildingsDictionary[target].gameObject.GetComponent<Place>().RepairBuilding(RepairAmount);
        GetComponentInParent<Animator>().SetTrigger("isRepearing");

    }
    IEnumerator RepairBuildingCoroutine()
    {

        yield return new WaitForSeconds(1.2f); 
        coroutine = null;
       
    }


}
