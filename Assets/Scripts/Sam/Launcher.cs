using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject loadingScreen;
    public GameObject menuButtons;
    public TMP_Text loadingText;

    public GameObject createRoomScreen;
    public TMP_InputField roomNameInput;
    public GameObject roomScreen;
    public TMP_Text roomRoomText, playerNameLabel;
    private List<TMP_Text> allPlayerNames= new List<TMP_Text>();    

    public GameObject errorScreen;
    public TMP_Text errorText;

    public GameObject roomBrowserScreen;
    public RoomButton theRoomButton;
    public List<RoomButton> allRoomButtons=new List<RoomButton>();

    public GameObject nameInputScreen;
    public TMP_InputField nameInput;
    public bool hasNickName;
    public GameObject startButton;


    public GameObject roomTestButton;

    public TMP_Text playersAmount;


    public string levelToPlay;
    private void Start()
    {

        CloseMenus();
        loadingScreen.SetActive(true);
        loadingText.text = "Buscando tierra plana...";

        PhotonNetwork.ConnectUsingSettings();

#if UNITY_EDITOR
        roomTestButton.SetActive(true);
#endif
    }
    public override void OnConnectedToMaster()
    {
        CloseMenus();
        PlayerPrefs.DeleteKey("role");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        loadingText.text = "Tierra a la vista...";

    }
    public override void OnJoinedLobby()
    {
        CloseMenus();
        menuButtons.SetActive(true);
        PhotonNetwork.NickName=Random.Range(0, 1000f).ToString();
        hasNickName=PlayerPrefs.HasKey("playerName");
        if (!hasNickName)
        {
            nameInputScreen.SetActive(true);
            if (PlayerPrefs.HasKey("playerName"))
            {
                nameInput.text = PlayerPrefs.GetString("playerName");
            }
        }
        else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("playerName");
        }

    }
    void CloseMenus()
    {
        loadingScreen.SetActive(false);
        menuButtons.SetActive(false);
        createRoomScreen.SetActive(false);
        errorScreen.SetActive(false);
        roomScreen.SetActive(false);
        roomBrowserScreen.SetActive(false);
        nameInputScreen.SetActive(false);
    }

    public void OpenRoomCreate()
    {
        PlayerPrefs.DeleteKey("role");
        CloseMenus();
        createRoomScreen.SetActive(true);
    }
    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInput.text)) {

            RoomOptions options =new RoomOptions();
            options.MaxPlayers = 4;

            PhotonNetwork.CreateRoom(roomNameInput.text,options);
            CloseMenus();
            loadingText.text = "Creando campamento...";
            loadingScreen.SetActive(true);
        }
    }
    public void JoinOwnRoom(CampButton campButton)
    {
        if (!string.IsNullOrEmpty(campButton.campName.text))
        {
            
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 4;

            PhotonNetwork.CreateRoom(campButton.campName.text, options);
            CloseMenus();
            loadingText.text = "Uniéndose a " + campButton.campName.text;
            loadingScreen.SetActive(true);
        }
    }
    public override void OnJoinedRoom()
    {
        playersAmount.text = PhotonNetwork.PlayerList.Length.ToString()+"/"+4 +" Jugadores";

        base.OnLeftRoom();
        CloseMenus();
        roomScreen.SetActive(true );
        string resourcesPath = "/resources" + PhotonNetwork.CurrentRoom.Name + ".json";
        PlayerPrefs.SetString("resources",resourcesPath);
        string buildingsPath = "/buildings" + PhotonNetwork.CurrentRoom.Name + ".json";
        PlayerPrefs.SetString("buildings", buildingsPath);

        roomRoomText.text = PhotonNetwork.CurrentRoom.Name;
        ListAllPlayers();

        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else 
        {
            startButton.SetActive(false);
        }
    }
    private void ListAllPlayers()
    {
        foreach(TMP_Text item in allPlayerNames)
        {
            Destroy(item.gameObject);

        }
        playerNameLabel.gameObject.SetActive(false);
        allPlayerNames.Clear();
        Player[] players = PhotonNetwork.PlayerList;
        for(int i = 0; i < players.Length; i++)
        {
            TMP_Text newPlayerLabel=Instantiate(playerNameLabel,playerNameLabel.transform.parent);
            newPlayerLabel.text = players[i].NickName;
            newPlayerLabel.gameObject.SetActive(true);
            allPlayerNames.Add(newPlayerLabel);

        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (newPlayer.IsLocal)
        {
            PlayerPrefs.DeleteKey("role");

        }
        playersAmount.text = PhotonNetwork.PlayerList.Length.ToString() + "/" + 4 + " Jugadores";

        ListAllPlayers();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (otherPlayer.IsLocal)
        {
            PlayerPrefs.DeleteKey("role");

        }
        playersAmount.text = PhotonNetwork.PlayerList.Length.ToString() + "/" + 4 + " Jugadores";

        ListAllPlayers();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        errorText.text = "Error al crear campamento: " + message;
        CloseMenus();
        errorScreen.SetActive(true);
    }

    public void CloseErrorScreen()
    {
        CloseMenus();
        menuButtons.SetActive(true);
    }

    public void LeaveRoomSesion()
    {
        PhotonNetwork.LeaveRoom();

        CloseMenus();
        loadingText.text = "Dejándo el campamento...";
        loadingScreen.SetActive(true);
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

        SceneManager.LoadScene(0);
        CloseMenus();
        menuButtons.SetActive(true) ;
    }
   
    public void OpenRoomBrowser()
    {
        PlayerPrefs.DeleteKey("role");
        CloseMenus();
        roomBrowserScreen.SetActive(true);
    }

    public void CloseRoomBrowser()
    {
        CloseMenus();
        menuButtons.SetActive(true);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)//Any time the rooms change
    {
        foreach (RoomButton item in allRoomButtons)
        {
            Destroy(item.gameObject);
        }
        allRoomButtons.Clear();
        theRoomButton.gameObject.SetActive(false);
        for (int i = 0; i < roomList.Count; i++)
        {
            if(roomList[i].PlayerCount!= roomList[i].MaxPlayers&& !roomList[i].RemovedFromList)
            {
                RoomButton newButton = Instantiate(theRoomButton, theRoomButton.transform.parent);
                newButton.SetButtonDetails(roomList[i]);
                newButton.gameObject.SetActive(true);
                allRoomButtons.Add(newButton);
            }
            
        }


    }

    public void JoinRoom(RoomInfo inputInfo)
    {
        PhotonNetwork.JoinRoom(inputInfo.Name);
        CloseMenus();
        loadingText.text = "Uniéndose al campamento";
        loadingScreen.SetActive(true);
    }
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(levelToPlay);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetNickName()
    {
        if (!string.IsNullOrEmpty(nameInput.text))
        {
            PhotonNetwork.NickName = nameInput.text;
            PlayerPrefs.SetString("playerName",nameInput.text);
            CloseMenus();
            menuButtons.SetActive(true);
            hasNickName = true;
        }
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.LeaveRoom();
    }
    public void QuickJoin()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 8;
        PhotonNetwork.CreateRoom("Test",options);
        CloseMenus();
        loadingText.text = "Creando  campamento";
        loadingScreen.gameObject.SetActive(true);
    }


    
}
