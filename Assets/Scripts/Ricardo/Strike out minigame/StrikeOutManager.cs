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
                players++;
           
                startButton.SetActive(true);
                Debug.Log(players+" Enter");
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
                Debug.Log(players+" out");
            }
        }
    }
    void StartMatch()
    {
            if (activeMatch == false)
            {
                startButton.SetActive(false);
                playerCount = players;
                Debug.Log(playerCount);
                
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

            StartCoroutine(ResetGame());
            endMatch.FireEvent();
            barrier.SetActive(false);
            activeMatch = false;

        }
    }


    IEnumerator ResetGame()
    {

        yield return new WaitForSeconds(0.4f);
        playerCount = 0;
        players = 0;
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
