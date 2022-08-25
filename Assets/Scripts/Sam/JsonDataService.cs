using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class JsonDataService : IDataService
{

    public bool SaveData<T>(string RelativePath, T Data, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;
        Debug.Log(path);
        try
        {

            if (File.Exists(path))
            {
                Debug.Log("Data exists. Deleting old file and writing a new one!");
                File.Delete(path);
            }
            else
            {
                Debug.Log("Writing file for the first time!");
            }
            using FileStream stream = File.Create(path); //Create a new .json file

            //Extra settings
            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(Data, settings));//Write the object to a json

            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
            return false;
        }
    }



    public T LoadData<T>(string RelativePath, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;

        if (!File.Exists(path))
        {
            Debug.LogError($"Cannot load file at {path}. File does not exist!");
            throw new FileNotFoundException($"{path} does not exist!");
        }

        try
        {
            T data;

            data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));

            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
            throw e;
        }
    }


}