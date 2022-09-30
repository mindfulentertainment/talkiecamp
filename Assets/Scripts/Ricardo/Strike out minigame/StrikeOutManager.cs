using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeOutManager : MonoBehaviour
{
    [SerializeField] int players,playerCount;
    [SerializeField] bool activeMatch;
    [SerializeField] REvents startMatch,playerOut,endMatch;
    [SerializeField] GameObject barrier,startButton;
    void Start()
    {
        players = 0;
        startMatch.GEvent += StartMatch;
        playerOut.GEvent += EndMatch;
        barrier.SetActive(false);
        startButton.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            if (activeMatch == false)
            {
                players++;
                startButton.SetActive(true);
                //Debug.Log(players);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            if (activeMatch == false)
            {
                players--;
                if (players == 0 )
                {
                    startButton.SetActive(false);
                }
                //Debug.Log(players);
            }
        }
    }
    void StartMatch()
    {
        
            if (activeMatch == false)
            {
                startButton.SetActive(false);
                playerCount = players;
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
            playerCount = 0;
            players = 0;
            endMatch.FireEvent();
            activeMatch = false;
            barrier.SetActive(false);
        }
    }

    
}
