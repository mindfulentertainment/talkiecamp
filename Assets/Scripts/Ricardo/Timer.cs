using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int minutes, seconds;
    [SerializeField] bool loop;
    [SerializeField] REvents startTimer,eventShoot,stopTime;
    
    [SerializeField] int m, s;
    
    // Start is called before the first frame update
    void Start()
    {
        startTimer.GEvent += StartTimer;
        stopTime.GEvent += StopTimer;

    }
    public void StartTimer() //empieza el contador
    {
        StopTimer();
        RestartTime();

        Invoke("UpdateTimer", 1f);
    }

    public void StopTimer() //detener el contador
    {
        
        CancelInvoke("UpdateTimer");
    }
    void UpdateTimer()
    {
        s--;
        if (s < 0)
        {
            if (m == 0)
            {
                eventShoot.FireEvent();
                RestartTime();
                if (loop == false)
                {
                    StopTimer();
                    return;
                }
            }
            else
            {
                m--;
                s = 59;
            }
        }

        Invoke("UpdateTimer", 1f);
    }
    void RestartTime()
    {
        m = minutes;
        s = seconds;
    }
    
    private void OnDestroy()
    {
        startTimer.GEvent -= StartTimer;
        stopTime.GEvent -= StopTimer;
    }
}
