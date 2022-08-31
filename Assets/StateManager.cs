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

    public UnityEvent<Resource> OnResourcesLoad = new UnityEvent<Resource>();
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
        string path = PlayerPrefs.GetString("resources");

        if (DataService.SaveData(path, DataManager.instance.resource, false)) return;

        Debug.LogError("Could not save file! ");

    }


    public void LoadData()
    {
        string path = PlayerPrefs.GetString("resources");

        try
        {
            string output = JsonConvert.SerializeObject(path);
            Resource data = DataService.LoadData<Resource>(path, false);
            OnResourcesLoad?.Invoke(data);
            Debug.Log("loadiing");

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
            yield return new WaitForSeconds(30);
            SerializeJson();
            Debug.Log("Saved");

        }
    }
    //Initially, there will be three json ---resources.json --buildingsprops.json--- gamestate.json
}

public class Resource
{
    public int stone;
    public int concrete;
    public int wood;
    public Food[] food;
    public int meat;
    public int bread;
    public int leaf;
    public int water;
    public int cheese;

}
public class Food
{
    public int hamburguer;
    public int sandwich;
    public int juice;
    public int soup;

}