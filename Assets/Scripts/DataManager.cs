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
    public Dictionary<string, GameObject> buildingsDictionary = new Dictionary<string, GameObject>();

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
            for (int i = 0; i < buildings.buildings.Count; i++)
            {
                GameObject newBuilding = PhotonNetwork.Instantiate(buildings.buildings[i].buildingName, buildings.buildings[i].GetPosition(), buildings.buildings[i].GetRotation());
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
                case Element.ElementType.connection:
                    resource.connection += element.GetAmount();
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

                case Element.ElementType.ham:
                    resource.ham += element.GetAmount();
                    break;

                case Element.ElementType.lettuce:
                    resource.lettuce += element.GetAmount();
                    break;

                case Element.ElementType.carrot:
                    resource.carrot += element.GetAmount();
                    break;

                case Element.ElementType.bun:
                    resource.bun+= element.GetAmount();
                    break;

                case Element.ElementType.tomato:
                    resource.tomato += element.GetAmount();
                    break;         

                case Element.ElementType.sandwich:
                    resource.food.sandwich += element.GetAmount();
                    break;

                case Element.ElementType.sandwich_2:
                     resource.food.sandwich2 += element.GetAmount();
                    break;

                case Element.ElementType.hamburguer:
                    resource.food.hamburguer += element.GetAmount();
                    break;

                case Element.ElementType.hamburguer_2:
                    resource.food.hamburguer2 += element.GetAmount();
                    break;

                case Element.ElementType.soup:
                    resource.food.soup += element.GetAmount();
                    break;

                case Element.ElementType.soup_2:
                    resource.food.soup2 += element.GetAmount();
                    break;
            }

        
        UIController.instance.ChangeResources(resource);

    }
   
    public void NewBuilding()
    {
        OnNewBuilding?.Invoke(resource, buildings);

    }
    public void DecreaseElement(Element element)
    {
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

                case Element.ElementType.soup:
                    resource.food.soup -= element.GetAmount();
                    break;

                case Element.ElementType.ham:
                    resource.ham -= element.GetAmount();
                    break;

                case Element.ElementType.lettuce:
                    resource.lettuce -= element.GetAmount();
                    break;

                case Element.ElementType.carrot:
                    resource.carrot -= element.GetAmount();
                    break;

                case Element.ElementType.bun:
                    resource.bun -= element.GetAmount();
                    break;

                case Element.ElementType.tomato:
                    resource.tomato -= element.GetAmount();
                    break;

                case Element.ElementType.sandwich_2:
                    resource.food.sandwich2 -= element.GetAmount();
                    break;

                case Element.ElementType.hamburguer_2:
                    resource.food.hamburguer2 -= element.GetAmount();
                Debug.Log(element.GetAmount());
                    break;
         
                case Element.ElementType.soup_2:
                    resource.food.soup2 -= element.GetAmount();
                    break;
            }

        UIController.instance.ChangeResources(resource);
    }

    public bool CheckResourceAmount(string resourceName)
    {
        Element resourceElement = null;

        switch (resourceName)
        {
            case "meat":
                if (resource.meat > 0)
                {
                    resourceElement = new Element(Element.ElementType.meat, 1);
                    DecreaseElement(resourceElement);
                    return true;
                }
                break;

            case "ham":
                if (resource.ham > 0)
                {
                    resourceElement = new Element(Element.ElementType.ham, 1);
                    DecreaseElement(resourceElement);
                    return true;
                }
                break;

            case "lettuce":
                if (resource.lettuce > 0)
                {
                    resourceElement = new Element(Element.ElementType.lettuce, 1);
                    DecreaseElement(resourceElement);
                    return true;
                }
                break;

            case "carrot":
                if (resource.carrot > 0)
                {
                    resourceElement = new Element(Element.ElementType.carrot, 1);
                    DecreaseElement(resourceElement);
                    return true;
                }
                break;

            case "bun":
                if (resource.bun > 0)
                {
                    resourceElement = new Element(Element.ElementType.bun, 1);
                    DecreaseElement(resourceElement);
                    return true;
                }
                break;

            case "tomato":
                if (resource.tomato > 0)
                {
                    resourceElement = new Element(Element.ElementType.tomato, 1);
                    DecreaseElement(resourceElement);
                    return true;
                }
                break;

            default:
                return false;
        }

        return false;
    }
}
