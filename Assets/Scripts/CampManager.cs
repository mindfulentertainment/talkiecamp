using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using TMPro;
using System.IO;

public class CampManager : MonoBehaviour
{
    private IDataService DataService = new JsonDataService();
    public string campJson = "/camps.json";
    public List<Camp> camps = new List<Camp>();
    public GameObject button;
    public void SerializeJson(List<Camp> camps)
    {
        if (DataService.SaveData(campJson, camps, false)) return;


    }
    private void Start()
    {

        LoadData();
        Destroy(button.gameObject);

    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + campJson;
        if (!File.Exists(path)) return;
        try
        {
            string output = JsonConvert.SerializeObject(campJson);
            List<Camp> data = DataService.LoadData<List<Camp>>(campJson, false);
            for (int i = 0; i < data.Count; i++)
            {
                Camp camp = (Camp)data[i];
                camps.Add(camp);
            }
            for (int i = 0; i < camps.Count; i++)
            {

                GameObject newButton = Instantiate(button, button.transform.parent);
                string campname = camps[i].campName;
                newButton.GetComponent<CampButton>().SetButtonDetails(campname);



            }

        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }

    }
    public void CreateResources(string campName)
    {
        string path = "/resources"+ campName+".json";
        Resource resource = new Resource();
        if (DataService.SaveData(path, resource, false)) return;


    }
    public void CreateBuildings(string campName)
    {
        string path = "/buildings" + campName + ".json";
        Resource resource = new Resource();
        if (DataService.SaveData(path, resource, false)) return;


    }
    public void CreateCamp()
    {
        Camp camp = new Camp(Launcher.instance.roomNameInput.text); 
        camps.Add(camp);
        SerializeJson(camps);
        CreateResources(camp.campName);
        CreateBuildings(camp.campName);
    }
}

public class Camp
{
    public Camp(string campName)
    {
        this.campName = campName;
        resource = new Resource();
    }
    public string campName;
    public Resource resource;
}