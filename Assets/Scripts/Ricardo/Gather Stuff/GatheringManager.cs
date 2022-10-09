using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GatheringManager : MonoBehaviour
{
    [SerializeField] TimerGather temporizador;
    [SerializeField] int objectCountIni,c;
    [SerializeField] TextMeshProUGUI textMark;
    [SerializeField] REvents finishGathering,updateTime,deductCount,starGathering,disappear;
    void Start()
    {
        c = objectCountIni;
        updateTime.GEvent += UpdateTime;
        deductCount.GEvent += DeductCount;
        starGathering.GEvent += StartGame;
        disappear.GEvent += Disappear;
        textMark.gameObject.SetActive(false);
    }
    void StartGame()
    {
        
        textMark.gameObject.SetActive(true);
        
    }
    void Disappear()
    {
        textMark.gameObject.SetActive(false);
    }
    void UpdateTime()
    {
        if (temporizador.s < 10)
        {
            textMark.text = "0" + temporizador.m + " : 0" + temporizador.s;
        }
        else
        {
            textMark.text = "0" + temporizador.m + " : " + temporizador.s;
        }
    }
    void DeductCount()
    {
        c--;
        if (c <= 0)
        {
            finishGathering.FireEvent();
            c = objectCountIni;
            textMark.gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        updateTime.GEvent -= UpdateTime;
        deductCount.GEvent -= DeductCount;
        starGathering.GEvent -= StartGame;
        disappear.GEvent -= Disappear;
    }

}
