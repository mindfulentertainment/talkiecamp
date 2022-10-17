using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearStartButton : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] int count;
    [SerializeField] bool activeGame;
    [SerializeField] REvents startGame, endGame;
    [SerializeField] Vector3 initialSize;
    [SerializeField] float appearTime;
    private void Start()
    {
        initialSize = button.transform.localScale;
        button.transform.localScale = Vector3.zero;
        //button.SetActive(false);
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
                //button.SetActive(true);
                button.transform.LeanScale(initialSize, appearTime).setEaseOutQuart();
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (activeGame == false)
        {
            if (other.CompareTag("PlayerBody"))
             {
            
                count--;
                if (count <= 0)
                {
                    //button.SetActive(false);
                    button.transform.LeanScale(Vector3.zero, appearTime).setEaseOutQuart();
                }
            }
        }
    }
    void ActiveMatch()
    {
        activeGame = true;
        //button.SetActive(false);
        button.transform.LeanScale(Vector3.zero, appearTime).setEaseOutQuart();
    }
    void DeactivateMatch()
    {
        activeGame = false;
        count = 0;
        button.transform.LeanScale(initialSize, appearTime).setEaseOutQuart();
    }
    private void OnDestroy()
    {
        startGame.GEvent -= ActiveMatch;
        endGame.GEvent -= DeactivateMatch;
    }
}
