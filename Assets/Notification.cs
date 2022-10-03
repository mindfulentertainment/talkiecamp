using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Notification : MonoBehaviour
{
    public static Notification instance;
    public GameObject notificationFrame;
    public TMP_Text message;
    private Coroutine frameOff;
    
    private void Awake()
    {
        instance = this;
    }


    public void ShowMessage(string m, float time)
    {
        notificationFrame.gameObject.SetActive(true);
        message.text = m;
        if (frameOff != null)
        {
            StopCoroutine(frameOff);

        }
        frameOff =StartCoroutine(TurnOffFrame(time));
    }

    IEnumerator TurnOffFrame(float time)
    {
        yield return new WaitForSeconds(time);
        notificationFrame.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        notificationFrame.gameObject.SetActive(true);
        message.text = "Hay más eventos disponiles. Acércate al organizador";
        yield return new WaitForSeconds(5);
        notificationFrame.gameObject.SetActive(false);

    }
}
