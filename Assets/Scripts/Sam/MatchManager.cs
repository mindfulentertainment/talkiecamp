using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MatchManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public static MatchManager instance;

    public enum EventCodes : byte
    {
        NewPlayer,
        ListPlayers
        
    }
    public List<PlayerInfo> allPlayers = new List<PlayerInfo>();
    private int index;




    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            NewPlayerSend(PhotonNetwork.NickName);
        }


    }

   
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);

    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code < 200)
        {
            EventCodes theEvent=(EventCodes)photonEvent.Code;
            object[] data=(object[])photonEvent.CustomData;
            switch (theEvent)
            {
                case EventCodes.NewPlayer:
                    NewPlayerReceive(data);
                    break;
                case EventCodes.ListPlayers:
                    ListPlayersReceive(data);
                    break ;
              

            }
        }
    }

    public void NewPlayerSend(string username)
    {
        object[] package = new object[4];
        package[0] = username;
        package[1] = PhotonNetwork.LocalPlayer.ActorNumber;
        package[2] = 0;
        package[3] = 0;

        PhotonNetwork.RaiseEvent(
            (byte)EventCodes.NewPlayer,package,new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient },new SendOptions {Reliability=true });
    }
    public void NewPlayerReceive(object[] dataRecived)
    {
        PlayerInfo player = new PlayerInfo((string)dataRecived[0], (int)dataRecived[1], (int)dataRecived[2], (int)dataRecived[3]);
        allPlayers.Add(player);
        ListPlayersSend();
    }
    public void ListPlayersSend()
    {
        object[] package=new object[allPlayers.Count];
        for (int i = 0; i < allPlayers.Count; i++)
        {
            object[] piece = new object[4];

            piece[0] = allPlayers[i].name;
            piece[1] = allPlayers[i].actor;
            piece[2] = allPlayers[i].kills;
            piece[3] = allPlayers[i].deaths;

            package[i]= piece;
        }
        PhotonNetwork.RaiseEvent(
            (byte)EventCodes.ListPlayers, package, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });

    }
    public void ListPlayersReceive(object[] dataRecived)
    {
        allPlayers.Clear();
        for (int i = 0;i < dataRecived.Length;i++)
        {
            object[] piece = (object[])dataRecived[i];
            PlayerInfo player = new PlayerInfo((string)piece[0], (int)piece[1], (int)piece[2], (int)piece[3]);
            allPlayers.Add(player);

            if (PhotonNetwork.LocalPlayer.ActorNumber == player.actor)
            {
                index=i;
            }
        }
    }
  
 
   
}

    [System.Serializable]   
    public class PlayerInfo
    {
        public string name;
        public int actor, kills, deaths;

        public PlayerInfo(string name,  int actor, int kills, int deaths)
        {
            this.name = name;
            this.actor = actor;
            this.kills = kills;
            this.deaths = deaths;
        }
    }