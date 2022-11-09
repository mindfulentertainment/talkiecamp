using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class AnimalSpawner : MonoBehaviourPun
{
    public GameObject[] Animals;
    public float time;
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(SpawnAnimal());

        }
    }
    
     IEnumerator SpawnAnimal()
    {

        while (true)
        {
            int animal = Random.Range(0, Animals.Length);
            photonView.RPC("Spawn", RpcTarget.AllViaServer, animal);
            animal = Random.Range(1, Animals.Length)-1;
            photonView.RPC("Spawn", RpcTarget.AllViaServer, animal);
            yield return new WaitForSeconds(time);

        }
    }
    [PunRPC]
    private void Spawn(int animal)
    {
        Animals[animal].SetActive(true);
    }
}
