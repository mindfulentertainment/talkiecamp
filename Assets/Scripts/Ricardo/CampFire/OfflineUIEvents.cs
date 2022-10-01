using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineUIEvents : MonoBehaviour
{
    [SerializeField] REvents gEvent;
    public void FireButton()
    {
        gEvent.FireEvent();
    }


}
