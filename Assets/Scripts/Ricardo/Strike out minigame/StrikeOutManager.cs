using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeOutManager : MonoBehaviour
{
    [SerializeField] int players,playerCount;
    [SerializeField] bool activeMatch;
    [SerializeField] REvents startMatch,playerOut,endMatch;
    [SerializeField] GameObject barrier,startButton;
    [SerializeField] public Transform losingPos;

    List<GameObject>  playersList= new List<GameObject>();
    void Start()
    {
        players = 0;
        startMatch.GEvent += StartMatch;
        playerOut.GEvent += EndMatch;
        barrier.SetActive(false);
        startButton.SetActive(false);
        Invoke("AddTp", 2);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            if (activeMatch == false)
            {
                playersList.Add(other.gameObject);
                startButton.SetActive(true);
                Debug.Log(players+" Enter");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            playersList.Remove(other.gameObject);

            if (activeMatch == false)
            {

                if (playersList.Count<=0)
                {
                    startButton.SetActive(false);
                }
                Debug.Log(players+" out");
            }
        }
    }
    void StartMatch()
    {
            if (activeMatch == false)
            {
                startButton.SetActive(false);
                playerCount = playersList.Count;
                
                
                activeMatch = true;
                barrier.SetActive(true);
            }
        
        else
        {
            EndMatch();
        }
    }
    void EndMatch()
    {
        playerCount--;
        if (playerCount <= 0)
        {
            Debug.Log("final");

            endMatch.FireEvent();
            barrier.SetActive(false);
            activeMatch = false;

        }
    }


    
    private void OnDestroy()
    {
        startMatch.GEvent -= StartMatch;
        playerOut.GEvent -= EndMatch;
    }

   
    void AddTp()
    {
        PlayerTP tp = PlayerSpawner.instance.player.AddComponent<PlayerTP>();
        tp.losingPos = losingPos;
    }

}
