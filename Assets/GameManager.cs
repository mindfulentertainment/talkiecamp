using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tutorial;
    private void Start()
    {
        StateManager.Instance.OnResourcesLoad.AddListener(ReceiveData);
    }



    private void OnDisable()
    {
        StateManager.Instance.OnResourcesLoad.RemoveListener(ReceiveData);

    }
    private void ReceiveData(Resource resource, Buildings buildings)
    {
        if (!resource.newCamp)
        {
            tutorial.SetActive(true);
        }
    }

}
