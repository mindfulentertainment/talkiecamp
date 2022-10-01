using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendCalls : MonoBehaviour
{
    public static FriendCalls instance;



    [SerializeField] REvents[] friendCalls;


    public void CallFriend(string friendName)
    {

        switch (friendName)
        {
            case "Builder":
                friendCalls[0].FireEvent();
                break;
            case "Gatherer":
                friendCalls[1].FireEvent();
                break;
            case "Chef":
                friendCalls[2].FireEvent();
                break;
            case "Watchmen":
                friendCalls[3].FireEvent();
                break;
            case "Music":
                friendCalls[4].FireEvent();
                break;
        }
    }

    private void Awake()
    {
        instance = this;
    }
}
