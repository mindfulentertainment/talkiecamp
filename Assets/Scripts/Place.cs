using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.UI;

public class Place : MonoBehaviourPun
{

    public PlaceInfo PlaceInfo;
    Food food;
    public static Action<string> OnPlaceBuild;
    Resource resource;
    public float maxHealth;
    public float health;
    public Slider health_Slider;
    public BuildingHistory buildingHistory;
    private void OnEnable()
    {
        var photonV = GetComponent<PhotonView>();
        Destroy(photonV);
        if (Time.timeSinceLevelLoad > 3)
        {
            UseMaterials();
            BuildingHistory newBuilding = new BuildingHistory();
            newBuilding.buildingName = PlaceInfo.name;
            newBuilding.x = transform.position.x;
            newBuilding.y = transform.position.y;
            newBuilding.z = transform.position.z;
            newBuilding.q_x = transform.rotation.x;
            newBuilding.q_y = transform.rotation.y;
            newBuilding.q_z = transform.rotation.z;
            newBuilding.q_w = transform.rotation.w;

            DataManager.instance.buildings.buildings.Add(newBuilding);
        }
        OnPlaceBuild?.Invoke(PlaceInfo.placeName);
        Vector3 keyVector= new Vector3(gameObject.transform.position.x, 0.01f, gameObject.transform.position.z);
        string key= keyVector.ToString();
        Collider col = DataManager.instance.keyValuePairs[key];


        col.enabled = false;
        col.gameObject.GetComponentInChildren<MeshRenderer>().material = DataManager.instance.unavailable;
        DataManager.instance.NewBuilding();
    }
    private void Start()
    {
        DataManager.instance.buildingsDictionary.Add(this.transform.position.ToString(), this.gameObject);
        health = maxHealth;

        health_Slider.value=health/maxHealth;
        health_Slider.gameObject.SetActive(false);

    }


   
    public void DamageBuilding(float damage)
    {
        health_Slider.gameObject.SetActive(true);
        health -= damage;
        health_Slider.value = health / maxHealth;
        if (health <= 0)
        {
            Destroy(this.gameObject,0.5f);

            BuildingHistory bh = null;

            foreach (var item in DataManager.instance.buildings.buildings)
            {
                if (item.GetPosition() == buildingHistory.GetPosition())
                {
                    bh=item;
                }
            }
            if (bh != null)
            {
                DataManager.instance.buildings.buildings.Remove(bh);
                Debug.Log("ELIMINADO");
            }

        }
    }

    void UseMaterials()
    {

        food = new Food(PlaceInfo.hamburguers, PlaceInfo.sandwiches, PlaceInfo.soups);

        resource = new Resource(PlaceInfo.stone, PlaceInfo.fabric, PlaceInfo.wood, food);
        resource.connection = PlaceInfo.conexion;
        if (resource.stone > 0)
        {
            Element element = new Element(Element.ElementType.stone, resource.stone);
            DataManager.instance.DecreaseElement(element);
        }
        if (resource.connection > 0)
        {
            Element element = new Element(Element.ElementType.connection, resource.connection);
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

    public void RepairBuilding(float amount)
    {
        health+=amount;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        health_Slider.value = health / maxHealth;


    }

}
