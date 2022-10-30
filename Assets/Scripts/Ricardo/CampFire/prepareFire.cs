using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class prepareFire : MonoBehaviour
{
    [SerializeField] int woodCount,maxWood;
    [SerializeField] TextMeshProUGUI woodText;
    [SerializeField] GameObject bar;
    [SerializeField] ParticleSystem fire;
    [SerializeField] REvents fireOn, fireOff,instructionWood,instructionFire;
    
    [SerializeField] Vector3 inititalBarSize;
    [SerializeField] float appearTime;

    
    void Start()
    {
       
        inititalBarSize = bar.transform.localScale;
        woodCount = 0;
        woodText.text = woodCount + " / " + maxWood;
        //bar.SetActive(false);
        bar.transform.localScale = Vector3.zero;
        fire.gameObject.SetActive(false);
        fireOn.GEvent += FireOn;
        fireOff.GEvent += FireOff;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            other.gameObject.SetActive(false);
            WoodCount();
            Debug.Log(woodCount);
        }
        if (other.CompareTag("PlayerBody"))
        {
            
            if (woodCount >= maxWood)
            {
                //bar.SetActive(true);
                bar.transform.LeanScale(inititalBarSize, appearTime).setEaseOutQuart();
                instructionFire.FireEvent();
            }
            else
            {
                instructionWood.FireEvent();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            //bar.SetActive(false);
            bar.transform.LeanScale(Vector3.zero, appearTime).setEaseOutQuart();
        }
    }
    void WoodCount()
    {
        woodCount++;
        if (woodCount >= maxWood)
        {
            woodCount = maxWood;

            //bar.SetActive(true);
            bar.transform.LeanScale(inititalBarSize, appearTime).setEaseOutQuart();
        }
        woodText.text = woodCount + " / " + maxWood;
    }
    void FireOn()
    {
        fire.gameObject.SetActive(true);
        fire.Play(true);
    }
    void FireOff()
    {
        //fire.Stop();
        //fire.gameObject.SetActive(false);
        //woodCount = 0;
        //woodText.text = woodCount + " / " + maxWood;
    }
    private void OnDestroy()
    {
        fireOn.GEvent -= FireOn;
        fireOff.GEvent -= FireOff;
    }
}
