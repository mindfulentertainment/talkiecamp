using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Builder : PlayerController
{
    [PunRPC]
    public override void HandlePickUp(int key)
    {
        base.HandlePickUp(key);    
    }
    public override void OnEnable()
    {
        base.OnEnable();
        UIController.instance.storeButton.GetComponent<Button>().onClick.AddListener(StartToBuild);
    }

    public  void StartToBuild()
    {
        isBuilding = true;
        UIController.instance.storeButton.SetActive(false);
    }
}
