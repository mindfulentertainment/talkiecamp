using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public Resource resource = new Resource();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StateManager.Instance.OnResourcesLoad.AddListener(ReceiveData);
    }
    private void OnDisable()
    {
        StateManager.Instance.OnResourcesLoad.RemoveListener(ReceiveData);

    }
    void ReceiveData(Resource resource)
    {
        this.resource=resource;
        UIController.instance.ChangeResources(resource);

    }
    public void IncreaseResources()
    {
        resource.meat++;
        resource.leaf++;
        resource.bread++;
        UIController.instance.ChangeResources(resource);
    }
}
