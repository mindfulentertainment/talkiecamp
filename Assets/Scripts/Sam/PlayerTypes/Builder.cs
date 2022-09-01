using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Builder : PlayerController
{
    [PunRPC]
    public override void HandlePickUp()
    {
        base.HandlePickUp();    
    }
    private void OnEnable()
    {
        UIController.instance.storeButton.GetComponent<Button>().onClick.AddListener(StartToBuild);
    }

    public  void StartToBuild()
    {
        isBuilding = true;
        UIController.instance.storeButton.SetActive(false);
    }
}
