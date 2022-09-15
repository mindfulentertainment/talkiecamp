using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public Material available;
    public Material unavailable;
    public Buildings buildings= new Buildings();
    public Resource resource = new Resource();
    public Dictionary<string,Collider> keyValuePairs = new Dictionary<string,Collider>();
    public GameObject gridHexagones;

    public Action<Resource, Buildings> OnNewBuilding;

    private void Awake()
    {
        instance = this;
        foreach (Transform hexagone in gridHexagones.transform)
        {
            keyValuePairs.Add(hexagone.position.ToString(), hexagone.gameObject.GetComponent<Collider>());
            if (hexagone.gameObject.GetComponent<Collider>().enabled == false)
            {
                hexagone.gameObject.GetComponentInChildren<MeshRenderer>().material = unavailable;
            }
        }
    }
    private void Start()
    {
        StateManager.Instance.OnResourcesLoad.AddListener(ReceiveData);
    }
    private void OnDisable()
    {
        StateManager.Instance.OnResourcesLoad.RemoveListener(ReceiveData);

    }
    void ReceiveData(Resource resource, Buildings buildingHistory)
    {
        if(buildingHistory == null)
        {
        }
        for (int i = 0; i < buildingHistory.buildings.Count; i++)
        {
            buildings.buildings.Add(buildingHistory.buildings[i]);
        }
        this.resource=resource;
        UIController.instance.ChangeResources(resource);
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (var item in buildings.buildings)
            {

                PhotonNetwork.Instantiate(item.buildingName, item.GetPosition(), item.GetRotation());
                
            }
        }
           
           
        
    }
  
  
    public void IncreaseElement(Element element)
    {


       
            switch (element.type)
            {
                case Element.ElementType.wood:
                    resource.wood += element.GetAmount();

                    break;

                case Element.ElementType.stone:
                    resource.stone += element.GetAmount();

                    break;
                case Element.ElementType.fabric:
                    resource.fabric += element.GetAmount();

                    break;
                case Element.ElementType.meat:
                    resource.meat += element.GetAmount();

                    break;

                case Element.ElementType.connection:
                    resource.connection += element.GetAmount();

                    break;

                case Element.ElementType.sandwich:
                    resource.food.sandwich += element.GetAmount();

                    break;
                case Element.ElementType.hamburguer:
                    resource.food.hamburguer += element.GetAmount();
                    break;
            }

        
        UIController.instance.ChangeResources(resource);

    }
    public void DecreaseElement(Element element)
    {

        OnNewBuilding?.Invoke(resource, buildings);


            switch (element.type)
            {
                case Element.ElementType.wood:
                    resource.wood -= element.GetAmount();
                    Debug.Log(element.type + "" + element.GetAmount());

                    break;

                case Element.ElementType.stone:
                    resource.stone -= element.GetAmount();

                    break;
                case Element.ElementType.fabric:
                    resource.fabric -= element.GetAmount();

                    break;
                case Element.ElementType.meat:
                    resource.meat -= element.GetAmount();

                    break;

                case Element.ElementType.connection:
                    resource.connection -= element.GetAmount();

                    break;

                case Element.ElementType.sandwich:
                    resource.food.sandwich -= element.GetAmount();

                    break;
                case Element.ElementType.hamburguer:
                    resource.food.hamburguer -= element.GetAmount();
                    break;
            }

        

    }

}
