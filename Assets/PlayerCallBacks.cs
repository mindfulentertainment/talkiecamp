using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCallBacks : MonoBehaviourPun
{

    public void Emoticon(int index)
    {
        photonView.RPC("ShowEmoji", RpcTarget.AllBuffered, index);
    }
    
    [PunRPC]
   public void ShowEmoji(int index)
    {
        GetComponentInChildren<EmotionDisplay>().GiveEmotion(index);

    }
}
