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
    [SerializeField] Vector3 counterSize;
    
    void Start()
    {
        counterSize = textMark.transform.localScale;
        c = objectCountIni;
        updateTime.GEvent += UpdateTime;
        deductCount.GEvent += DeductCount;
        starGathering.GEvent += StartGame;
        disappear.GEvent += Disappear;
        //textMark.gameObject.SetActive(false);
        textMark.transform.localScale = Vector3.zero;

    }
    void StartGame()
    {
        
        //textMark.gameObject.SetActive(true);
        textMark.transform.LeanScale(counterSize, 0.3f).setEaseOutQuart();

    }
    void Disappear()
    {
        //textMark.gameObject.SetActive(false);
        textMark.transform.LeanScale(Vector3.zero, 0.3f).setEaseOutQuart();
        textMark.text = "0" + 1 + " : 0" + 0;
        finishGathering.FireEvent();
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
            //textMark.gameObject.SetActive(false);
            textMark.transform.LeanScale(counterSize, 0.3f).setEaseOutQuart();
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
