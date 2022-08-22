using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : PlayerController
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Store"))
        {
            UIController.instance.storeButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Store"))
        {
            UIController.instance.storeButton.SetActive(false);
        }
    }

}
