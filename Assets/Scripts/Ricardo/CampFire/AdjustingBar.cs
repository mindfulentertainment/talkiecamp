using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustingBar : MonoBehaviour
{
    [SerializeField] float  valueAdded, decreaseRate,beforeLigh,afterLight;
    [SerializeField] REvents increaseFireBar,fireOn,fireOff;
    [SerializeField] Slider bar;
    [SerializeField] Color low, medium, high,off,on;
    [SerializeField] Image barColor, icon;
    
 
    void Start()
    {
        decreaseRate = beforeLigh;
        bar.value = 0;
        increaseFireBar.GEvent += IncreaseBar;
    }

  
    void Update()
    {
        
        bar.value -= decreaseRate;
        if (bar.value <= 0)
        {
            bar.value = 0;
            decreaseRate = beforeLigh;
            fireOff.FireEvent();
            icon.color = off;
        }
        if (bar.value <= 0.25) {
            barColor.color = low;
        }
        else if(bar.value > 0.25 && bar.value <= 0.75)
        {
            barColor.color = medium;
        }
        else if(bar.value > 0.75)
        {
            barColor.color = high;
        }

    }
    void IncreaseBar()
    {
        
        bar.value += valueAdded;
        if (bar.value >= 1)
        {
            bar.value = 1;
            decreaseRate = afterLight;
            fireOn.FireEvent();
            icon.color = on;
        }
    }
    private void OnDestroy()
    {
        increaseFireBar.GEvent -= IncreaseBar;
    }
}
