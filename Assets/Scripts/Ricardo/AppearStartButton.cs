using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearStartButton : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] int count;
    [SerializeField] bool activeGame;
    [SerializeField] REvents startGame, endGame;
    private void Start()
    {
        button.SetActive(false);
        startGame.GEvent += ActiveMatch;
        endGame.GEvent += DeactivateMatch;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(activeGame == false)
        {
            if (other.CompareTag("PlayerBody"))
            {
                count++;
                button.SetActive(true);
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            count--;
            if (count <= 0)
            {
                button.SetActive(false);
            }
        }
    }
    void ActiveMatch()
    {
        activeGame = true;
        button.SetActive(false);
    }
    void DeactivateMatch()
    {
        activeGame = false;
    }
    private void OnDestroy()
    {
        startGame.GEvent -= ActiveMatch;
        endGame.GEvent -= DeactivateMatch;
    }
}
