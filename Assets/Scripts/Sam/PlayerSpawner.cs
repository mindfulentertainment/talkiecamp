using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner instance;

    private void Awake()
    {
        instance = this;
    }
    public GameObject chef;
    public GameObject watchmen;
    public GameObject gatherer;
    public GameObject builder;



    private GameObject player;
   
    
 
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer(PlayerPrefs.GetString("role"));
        }
    }

   void SpawnPlayer(string playerType )
    {
        Transform spawnPoint = SpawnManager.instance.GetRandomPosition();
        playerType.ToLower();
        switch (playerType)
        {
            case "chef":
                player = PhotonNetwork.Instantiate(chef.name, spawnPoint.position, spawnPoint.rotation);
                break;
            case "watchmen":
                player = PhotonNetwork.Instantiate(watchmen.name, spawnPoint.position, spawnPoint.rotation);
                break;
            case "gatherer":
                player = PhotonNetwork.Instantiate(gatherer.name, spawnPoint.position, spawnPoint.rotation);
                break;
            case "builder":
                player = PhotonNetwork.Instantiate(builder.name, spawnPoint.position, spawnPoint.rotation);
                break;

        }
    }


}
