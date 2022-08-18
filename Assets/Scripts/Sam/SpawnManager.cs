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

    public Transform GetRandomPosition()
    {
        Transform newTras = spawnTransform[Random.Range(0, spawnTransform.Length)];
        return newTras;
    }
   
}
