using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;
public class RolesManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    private Button roleChosen;
    public Button startGame;
    private float roles;
    public override void OnEnable()
    {
        startGame.interactable = true;
        PhotonNetwork.AddCallbackTarget(this);
        if (PhotonNetwork.IsMasterClient)
        {
            ResetRoles();

        }
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);

    }
    public List<Button> mButtons = new List<Button>();
    public enum EventCodes : byte
    {
        PlayerAction,
        RoleChanged


    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.LocalPlayer != newPlayer)
        {
            RolesSend();
        }
            
        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

        foreach (var item in mButtons)
        {
            if (item.transform.GetChild(1).GetComponent<TMP_Text>().text == otherPlayer.NickName)
            {
                item.interactable = true;
                item.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code < 200)
        {
            EventCodes theEvent = (EventCodes)photonEvent.Code;
            object[] data = (object[])photonEvent.CustomData;

            switch (theEvent)
            {
                case EventCodes.PlayerAction:
                    RolesRecieve(data);
                    break;
                case EventCodes.RoleChanged:
                    RolesChanged(data);
                    break;

            }
        }
    }

    public void RolesRecieve(object[] dataRecived)
    {
        for (int i = 0; i < dataRecived.Length; i++)
        {
            object[] piece = (object[])dataRecived[i];
            ButtonInfo buttonInfo = new ButtonInfo((string)piece[1], (bool)piece[0]);
            mButtons[i].interactable = buttonInfo.isInteractable;

            if (mButtons[i].interactable == false)
            {
                mButtons[i].gameObject.transform.GetChild(1).gameObject.SetActive(true);
                mButtons[i].gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = buttonInfo.playerName;
            }
            else
            {
                mButtons[i].gameObject.transform.GetChild(1).gameObject.SetActive(false);

            }


        }
        CheckRoles();


    }

    public void RolesChanged(object[] dataRecived)
    {
        string playerName = (string)dataRecived[0];
        int index = (int)dataRecived[2];
        int prevIndex = (int)dataRecived[3];

        if (prevIndex >= 0)
        {
            mButtons[prevIndex].interactable = true;
            mButtons[prevIndex].transform.GetChild(1).gameObject.SetActive(false);
        }
        

        mButtons[index].interactable = false;
        mButtons[index].transform.GetChild(1).GetComponent<TMP_Text>().text = playerName;
        mButtons[index].transform.GetChild(1).gameObject.SetActive(true);
       

    }
    public void ChangeRole(Button roleSelected = null)
    {
        object[] package = new object[4];
        for (int i = 0; i < mButtons.Count; i++)
        {

            if (mButtons[i] == roleSelected)
            {

                package[0] = PhotonNetwork.NickName;
                package[1] = false;
                package[2] = i;
                package[3] = mButtons.IndexOf(roleChosen);
            }

        }
        if (roleSelected != null)
        {
            if (roleChosen != null)
            {
                roleChosen.interactable = true;
                roleChosen.transform.GetChild(1).gameObject.SetActive(false);

            }
            roleChosen = roleSelected;
            roleSelected.interactable = false;
            roleSelected.transform.GetChild(1).gameObject.SetActive(true);
            roleSelected.transform.GetChild(1).GetComponent<TMP_Text>().text = PhotonNetwork.NickName;
            string role = (string)roleChosen.transform.GetChild(0).GetComponent<TMP_Text>().text.ToString();
            PlayerPrefs.SetString("role", role);
            Debug.Log(PlayerPrefs.GetString("role"));
        }
       
        PhotonNetwork.RaiseEvent(
      (byte)EventCodes.RoleChanged, package, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
    }


    public void RolesSend()
    {
        

        object[] package = new object[mButtons.Count];
        for (int i = 0; i < mButtons.Count; i++)
        {
            object[] piece = new object[2];


                bool roleChosen = mButtons[i].interactable;

                piece[0] = roleChosen;
                string s = (string)mButtons[i].transform.GetChild(1).GetComponent<TMP_Text>().text.ToString();
                piece[1] = s;

            package[i] = piece;
        }     
       
            PhotonNetwork.RaiseEvent(
            (byte)EventCodes.PlayerAction, package, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });      
    }
    void ResetRoles()
    {
        foreach (var item in mButtons)
        {
            item.interactable = true;
            item.transform.GetChild(1).gameObject.SetActive(false);
        }
        
    }
   
    void CheckRoles()
    {
        roles = 0;
        foreach(var item in mButtons)
        {
            if (item.interactable==false)
            {
                roles++;
            }
            
        }
       
    }
}
[System.Serializable]
public class ButtonInfo
{
    public bool isInteractable;
    public string playerName;
    public int index;
    public int preIndex;
    public ButtonInfo(string playerName, bool isInteractable)  
    {
        this.isInteractable = isInteractable;
        this.playerName = playerName;

     
    }
}