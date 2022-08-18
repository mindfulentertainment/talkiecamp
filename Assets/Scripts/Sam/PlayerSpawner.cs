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
    public GameObject playerPrefab;
    private GameObject player;
    public float respawnTime = 5;
    
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }

   void SpawnPlayer()
    {
        Transform spawnPoint = SpawnManager.instance.GetRandomPosition();
        player=PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
    }


}
