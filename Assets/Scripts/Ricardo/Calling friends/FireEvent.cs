using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEvent : MonoBehaviour
{
    [SerializeField] REvents call; //a quien va a llamar
    public static FireEvent instance;
    private void Awake()
    {
        instance = this;
    }

    public void Fire()
    {
        MatchManager.instance.Fire();
    }
    public void ReceiveHelp()
    {

        call.FireEvent();
    }


}
