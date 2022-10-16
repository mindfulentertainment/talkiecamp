using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StrikeOutManager : MonoBehaviour
{
    [SerializeField] int players,playerCount;
    [SerializeField] bool activeMatch;
    [SerializeField] REvents startMatch,playerOut,endMatch,updateTime;
    [SerializeField] GameObject startButton;
    [SerializeField] public Transform losingPos;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TimerGather temporizador;
    List<GameObject>  playersList= new List<GameObject>();
    void Start()
    {
        players = 0;
        startMatch.GEvent += StartMatch;
        endMatch.GEvent += EndMatch;
        updateTime.GEvent += UpdateTime;
        timer.text = "0" + 1 + " : 0" + temporizador.s;
        timer.gameObject.SetActive(false);
        startButton.SetActive(false);
        Invoke("AddTp", 2);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            playersList.Add(other.gameObject);

            if (activeMatch == false)
            {
                startButton.SetActive(true);
                Debug.Log(players+" Enter");
            }
        }
    }
    void UpdateTime()
    {
        if (temporizador.s < 10)
        {
            timer.text = "0" + temporizador.m + " : 0" + temporizador.s;
        }
        else
        {
            timer.text = "0" + temporizador.m + " : " + temporizador.s;
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
        timer.gameObject.SetActive(true);
        timer.text = "0" + 1 + " : 0" + temporizador.s;
        if (activeMatch == false)
            {
                startButton.SetActive(false);
                playerCount = playersList.Count;
                Debug.Log("PLAYECOUNT" + playerCount);
                
                activeMatch = true;
                
            }
        
        else
        {
            EndMatch();
        }
    }
    void EndMatch()
    {
        //playerCount--;
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        timer.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        Debug.Log(playerCount + "/" + playersList.Count);

        
            Debug.Log("final");

            
            
            activeMatch = false;
            startButton.SetActive(true);

    }

    
    private void OnDestroy()
    {
        startMatch.GEvent -= StartMatch;
        endMatch.GEvent -= EndMatch;
        updateTime.GEvent -= UpdateTime;
    }

   
    void AddTp()
    {
        PlayerTP tp = PlayerSpawner.instance.player.AddComponent<PlayerTP>();
        tp.losingPos = losingPos;
    }

}
