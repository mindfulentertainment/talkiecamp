using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.Events;
using System;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    public UnityEvent<Resource, Buildings> OnResourcesLoad = new UnityEvent<Resource, Buildings>();
    private IDataService DataService = new JsonDataService();

    
    //With this data service i can do the following things:
    //Sava data 
    // Cast data from a json, you don't need system.serializable
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        StartCoroutine(SaveDataPeriodically());
    }

    public void SerializeJson()
    {
        string pathResources = PlayerPrefs.GetString("resources");
        string pathBuildings= PlayerPrefs.GetString("buildings");
        if (DataService.SaveData(pathResources, DataManager.instance.resource, false))
        if (DataService.SaveData(pathBuildings, DataManager.instance.buildings, false))return ;

        Debug.LogError("Could not save file! ");

    }


    public void LoadData()
    {
        string resourcesPath = PlayerPrefs.GetString("resources");
        string buildingsPath = PlayerPrefs.GetString("buildings");

        try
        {
            Resource resource = DataService.LoadData<Resource>(resourcesPath, false);
            Buildings buildingHistory = DataService.LoadData<Buildings>(buildingsPath, false);
            OnResourcesLoad?.Invoke(resource, buildingHistory);

        }
        catch (Exception e)
        {
            Debug.LogError($"Could not read file! Show something on the UI here!" +e.Message);
        }
    }


    IEnumerator SaveDataPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            SerializeJson();

        }
    }
    //Initially, there will be three json ---resources.json --buildingsprops.json--- gamestate.json
}

public class Resource
{

    public Resource() { }
    public Resource(int stone, int fabric, int wood, Food food) {

        this.stone = stone;
        this.fabric = fabric;
        this.wood = wood;
        this.food = food;
    }

    public int stone;
    public int fabric;
    public int wood;

    public Food food= new Food();
    public int meat;
    public int connection;

}

public class BuildingHistory
{
    public string buildingName;
    public float health;
    public float x;
    public float y;
    public float z;

    public float q_x;
    public float q_y;
    public float q_z;
    public float q_w;



    public Vector3 GetPosition()
    {
        return new Vector3(x, y, z);
    }
    public Quaternion GetRotation()
    {
        return new Quaternion(q_x, q_y, q_z, q_w);
    }
}

public class Buildings
{
    public List<BuildingHistory> buildings=new List<BuildingHistory>();
}


public class Food
{

    public Food() { }

    public Food(int hamburguer, int sandwich, int soup)
    {
        this.hamburguer = hamburguer;
        this.sandwich = sandwich;
        this.soup = soup;
    }
    public int hamburguer;
    public int sandwich;
    public int juice;
    public int soup;

}