using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeOutNetwork : MonoBehaviour
{

    [SerializeField] REvents gEvent; //a quien va a llamar
    public static StrikeOutNetwork instance;
    private void Awake()
    {
        instance = this;
    }

    public void RequestStartGame()
    {
        MatchManager.instance.RequestSrikeOutGameToBegin();
    }
    public void StartGame()
    {

        gEvent.FireEvent();
    }

}
