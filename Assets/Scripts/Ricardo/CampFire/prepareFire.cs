using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class prepareFire : MonoBehaviour
{
    [SerializeField] int woodCount,maxWood;
    [SerializeField] TextMeshProUGUI woodText;
    [SerializeField] GameObject bar;
    //[SerializeField] ParticleSystem fire;
    [SerializeField] REvents fireOn, fireOff;
    void Start()
    {
        woodCount = 0;
        woodText.text = woodCount + " / " + maxWood;
        bar.SetActive(false);
        //fire.gameObject.SetActive(false);
        fireOn.GEvent += FireOn;
        fireOff.GEvent += FireOff;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            other.gameObject.SetActive(false);
            WoodCount();
        }
        if (other.CompareTag("PlayerBody"))
        {
            if (woodCount >= maxWood)
            {
                bar.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            bar.SetActive(false);
        }
    }
    void WoodCount()
    {
        woodCount++;
        if (woodCount >= maxWood)
        {
            woodCount = maxWood;

            bar.SetActive(true);
        }
        woodText.text = woodCount + " / " + maxWood;
    }
    void FireOn()
    {
        //fire.gameObject.SetActive(true);
        //fire.Play();
    }
    void FireOff()
    {
        //fire.Stop();
        //fire.gameObject.SetActive(false);
        //woodCount = 0;
        woodText.text = woodCount + " / " + maxWood;
    }
    private void OnDestroy()
    {
        fireOn.GEvent -= FireOn;
        fireOff.GEvent -= FireOff;
    }
}
