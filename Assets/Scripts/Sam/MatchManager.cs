using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
public class MatchManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public static MatchManager instance;
    public static Action OnGameStart;
    public enum EventCodes : byte
    {
        NewPlayer,
        ListPlayers,
        UpdateResources
        
    }
    public List<PlayerInfo> allPlayers = new List<PlayerInfo>();
    private int index;

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if (allPlayers[i].name == otherPlayer.NickName)
            {
                allPlayers.RemoveAt(i);
            }
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UIController.instance.ScreenON();
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(LoadData());
        }
    }

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        StateManager.Instance.OnResourcesLoad.AddListener(SendResourcesInfo);

        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(LoadData());
        }

        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(NewPlayerSend(PhotonNetwork.NickName));
           
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

                case EventCodes.UpdateResources:
                    ResourcesReceive(data);
                    break;

            }
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        if (PlayerPrefs.HasKey("role"))
            {
            PlayerPrefs.DeleteKey("role");
        }
        SceneManager.LoadScene(0);
    }

    public void SendResourcesInfo(Resource resource, Buildings buildingHistory)
    {
        string jsonResources = JsonConvert.SerializeObject(resource);
        string jsonBuildings = JsonConvert.SerializeObject(buildingHistory);
        object[] package = new object[2];
        package[0] = jsonResources;
        package[1] = jsonBuildings;

        PhotonNetwork.RaiseEvent(
            (byte)EventCodes.UpdateResources, package, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
    }

    public void ResourcesReceive(object[] dataRecived)
    {
        string resourceData = (string)dataRecived[0];
        string buildingData = (string)dataRecived[1];
        Resource resource = JsonConvert.DeserializeObject<Resource>(resourceData);
        Buildings BuildingHistory= JsonConvert.DeserializeObject<Buildings>(buildingData);
        UIController.instance.ChangeResources(resource);
        DataManager.instance.buildings = BuildingHistory;
        
       OnGameStart?.Invoke();
    }

    IEnumerator NewPlayerSend(string username)
    {

        yield return new WaitForSeconds(0.1f);
        object[] package = new object[4];
        package[0] = username;
        package[1] = PhotonNetwork.LocalPlayer.ActorNumber;
        package[2] = PlayerPrefs.GetString("role");
        package[3] = 0;

        PhotonNetwork.RaiseEvent(
            (byte)EventCodes.NewPlayer,package,new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient },new SendOptions {Reliability=true });
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.LeaveRoom();
    }
    public void NewPlayerReceive(object[] dataRecived)
    {
        PlayerInfo player = new PlayerInfo((string)dataRecived[0], (int)dataRecived[1], (string)dataRecived[2], (int)dataRecived[3]);
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
            piece[2] = allPlayers[i].role;
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
            PlayerInfo player = new PlayerInfo((string)piece[0], (int)piece[1], (string)piece[2], (int)piece[3]);
            allPlayers.Add(player);

            if (PhotonNetwork.LocalPlayer.ActorNumber == player.actor)
            {
                index=i;
            }
        }
    }

    IEnumerator LoadData()
    {
        yield return new WaitForSeconds(1);
        StateManager.Instance.LoadData();
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
    }

    public void GoToMenu()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.LeaveRoom();
    }

}

    [System.Serializable]   
    public class PlayerInfo
    {
        public string name, role;
        public int actor, deaths;

        public PlayerInfo(string name,  int actor, string role, int deaths)
        {
            this.name = name;
            this.actor = actor;
            this.role = role;
            this.deaths = deaths;
        }
    }