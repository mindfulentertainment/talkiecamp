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
            case "Watchmen":
                newTras = spawnTransform[1];
                break;
            case "Builder":
                newTras = spawnTransform[2];
                break;
            case "Gatherer":
                newTras = spawnTransform[3];
                break;
            default: newTras = spawnTransform[0]; break;

        }
        return newTras;
    }
   
}
