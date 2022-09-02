using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Place : MonoBehaviourPun
{

    public PlaceInfo PlaceInfo;
    Food food;
    Resource resource;
    private void OnEnable()
    {
        var photonV = GetComponent<PhotonView>();
        Destroy(photonV);
        UseMaterials();
        BuildingHistory newBuilding = new BuildingHistory();
        newBuilding.buildingName = PlaceInfo.name;
        newBuilding.position=transform.position;
        newBuilding.rotation = transform.rotation;
        DataManager.instance.buildings.buildings.Add(newBuilding);

    }

    void UseMaterials()
    {

        food = new Food(PlaceInfo.hamburguers, PlaceInfo.sandwiches, PlaceInfo.soups);

        resource = new Resource(PlaceInfo.stone, PlaceInfo.fabric, PlaceInfo.wood, food);
        if (resource.stone > 0)
        {
            Element element = new Element(Element.ElementType.stone, resource.stone);
            DataManager.instance.DecreaseElement(element);
        }


        if (resource.wood > 0)
        {
            Element element = new Element(Element.ElementType.wood, resource.wood);
            DataManager.instance.DecreaseElement(element);
        }

        if (resource.fabric > 0)
        {
            Element element = new Element(Element.ElementType.fabric, resource.fabric);
            DataManager.instance.DecreaseElement(element);

        }

        if (food.sandwich > 0)
        {
            Element element = new Element(Element.ElementType.sandwich, food.sandwich);
            DataManager.instance.DecreaseElement(element);
        }
        if (food.hamburguer > 0)
        {
            Element element = new Element(Element.ElementType.hamburguer, food.hamburguer);
            DataManager.instance.DecreaseElement(element);
        }
        if (food.soup > 0)
        {
            Element element = new Element(Element.ElementType.soup, food.soup);
            DataManager.instance.DecreaseElement(element);
        }

        UIController.instance.ChangeResources(DataManager.instance.resource);

    }


}
