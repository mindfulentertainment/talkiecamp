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
        Debug.Log(playerType);
        switch (playerType)
        {
            case "Chef":
                player = PhotonNetwork.Instantiate(chef.name, spawnPoint.position, spawnPoint.rotation);
                break;
            case "Watchmen":
                player = PhotonNetwork.Instantiate(watchmen.name, spawnPoint.position, spawnPoint.rotation);
                break;
            case "Gatherer":
                player = PhotonNetwork.Instantiate(gatherer.name, spawnPoint.position, spawnPoint.rotation);
                break;
            case "Builder":
                player = PhotonNetwork.Instantiate(builder.name, spawnPoint.position, spawnPoint.rotation);
                break;

        }
    }


}
