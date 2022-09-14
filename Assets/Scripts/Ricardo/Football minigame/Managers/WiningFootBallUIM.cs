using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WiningFootBallUIM : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winingText;
    [SerializeField] REvents wT1, wT2;
    [SerializeField] string t1, t2;
    void Start()
    {
        winingText.gameObject.SetActive(false);
        wT1.GEvent += TeamOneWins;
        wT2.GEvent += TeamOneWins;
    }
    void TeamOneWins()
    {
        winingText.gameObject.SetActive(true);
        winingText.text = t1+" es el Ganador!!";
    }
    void TeamTwoWins()
    {
        winingText.gameObject.SetActive(true);
        winingText.text = t2 + " es el Ganador!!";
    }
    private void OnDestroy()
    {
        wT1.GEvent -= TeamOneWins;
        wT2.GEvent -= TeamOneWins;
    }
    private void OnDisable()
    {
        wT1.GEvent -= TeamOneWins;
        wT2.GEvent -= TeamOneWins;
    }
}
