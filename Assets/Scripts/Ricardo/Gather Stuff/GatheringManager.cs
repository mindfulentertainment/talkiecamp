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
    [SerializeField] TMP_Text counter;
    [SerializeField] ParticleSystem confeti;
    
    void Start()
    {
        counter.gameObject.SetActive(false);
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
        c = objectCountIni;
        Debug.Log("Game started");
        counter.gameObject.SetActive(true);
        counter.text = "Te faltan " + c + " pelotas por guardar";
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
        counter.text = "Llevas guardadas " + c + " pelotas de " + objectCountIni.ToString();

        if (c <= 0)
        {
            confeti.Play();
            counter.gameObject.SetActive(false);
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
