using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestComplete : MonoBehaviour
{
    public string message;

    private void OnEnable()
    {
        if (Time.timeSinceLevelLoad > 10)
        {
            Notification.instance.ShowMessage(message, 4);
        }
    }
}
