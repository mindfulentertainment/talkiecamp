using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnTransform;
    public static SpawnManager instance;
    private void Awake()
    {
        instance = this;    
    }
    private void Start()
    {
        foreach (Transform t in spawnTransform)
        {
            t.gameObject.SetActive(false);
        }
    }

    public Transform GetRolePosition()
    {
        string role = PlayerPrefs.GetString("role");
        Transform newTras;

        switch (role)
        {
            case "Chef":
                newTras = spawnTransform[0];
                break;
            case "Vigilante":
                newTras = spawnTransform[1];
                break;
            case "Constructor":
                newTras = spawnTransform[2];
                break;
            case "Recolector":
                newTras = spawnTransform[3];
                break;
            default: newTras = spawnTransform[0]; break;

        }
        return newTras;
    }
   
}
