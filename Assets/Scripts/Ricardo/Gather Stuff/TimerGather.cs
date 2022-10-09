using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerGather : MonoBehaviour
{
    public int minutes, seconds, m, s;
    [SerializeField] bool loop,aditionalCall;
    [SerializeField] REvents startTimer, eventShoot, stopTime,lastCall;



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
        if (aditionalCall == true)
        {
            lastCall.FireEvent();
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
