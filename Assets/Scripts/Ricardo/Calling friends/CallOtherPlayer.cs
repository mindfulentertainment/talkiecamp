using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallOtherPlayer : MonoBehaviour
{
    [SerializeField] REvents call; //a quien va a llamar
    // string name;  //es el nombre del boton al cual se llama
    
    // Update is called once per frame
    public void CallPlayer()
    {
        MatchManager.instance.CallFriend(call);
    }
}
