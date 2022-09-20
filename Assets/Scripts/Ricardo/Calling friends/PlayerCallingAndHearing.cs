using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCallingAndHearing : MonoBehaviour
{
    public bool recieveCall;
    [SerializeField] REvents hearingCall; //a que llamado escucha
    [SerializeField] GameObject arrowDir;

    void Start()
    {
        hearingCall.GEvent += RecieveCall;
    }
    void RecieveCall()
    {
        recieveCall = true;
        arrowDir.SetActive(true);
        Handheld.Vibrate();
        CameraShake.Shake(0.25f, 0.5f);
    }
    
    private void OnDestroy()
    {
        hearingCall.GEvent -= RecieveCall;
    }
}
