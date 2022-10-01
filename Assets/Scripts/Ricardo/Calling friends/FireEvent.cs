using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEvent : Photon.Pun.MonoBehaviourPun, IPunObservable
{
    [SerializeField] REvents call; //a quien va a llamar
    [SerializeField] AdjustingBar adjustingBar; //a quien va a llamar
    float value= 0; //a quien va a llamar
    public static FireEvent instance;
    private void Awake()
    {
        instance = this;
    }

    public void Fire()
    {
        value = adjustingBar.valueAdded;
        adjustingBar.bar.value += value;
    }
    public void ReceiveHelp()
    {

        call.FireEvent();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(value);
        }
        else
        {
            adjustingBar.bar.value += (float)stream.ReceiveNext();
        }
        value = 0;

    }
}
