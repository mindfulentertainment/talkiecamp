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
    private List<string> roles = new List<string>() { "Builder", "Gatherer", "Watchmen", "Chef" };


    public GameObject player;
   
    
 
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (PlayerPrefs.HasKey("role"))
            {
                SpawnPlayer(PlayerPrefs.GetString("role"));
            }
            else
            {
                StartCoroutine(SetPlayerRandomRole());
            }
        }
    }

   void SpawnPlayer(string playerType )
    {
        Transform spawnPoint = SpawnManager.instance.GetRolePosition();
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

    IEnumerator SetPlayerRandomRole()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < MatchManager.instance.allPlayers.Count; i++)
        {
            for (int j = 0; j < roles.Count; j++)
            {
                if (MatchManager.instance.allPlayers[i].role == roles[j])
                {
                    Debug.Log(roles[j]);
                    roles.RemoveAt(j);
                    
                }
            }
        }
           Debug.Log(string.Join(", ", roles));
        string newRole =roles[Random.Range(0, roles.Count)];
        Debug.Log(newRole);
        PlayerPrefs.SetString("role", newRole);
        SpawnPlayer(newRole);

    }

    public void ShowEmoticon(int index)
    {
        player.GetComponent<PlayerCallBacks>().Emoticon(index);
    }
    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(0, 10, 100, 32), "Vibrate!"))
    //    {
    //        Handheld.Vibrate();
    //        CameraShake.Shake(0.25f, 0.5f);
    //    }
           
    //}
}
