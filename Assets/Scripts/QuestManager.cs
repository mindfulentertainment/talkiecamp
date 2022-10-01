using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class QuestManager : MonoBehaviourPunCallbacks
{
    public Button questButton;
    public QuestInfo[] QuestInfo;
    public TMP_Text tmp_Description;
    public TMP_Text tmp_title;
    public GameObject bear;
    int actualQuest=0;
    public override void OnEnable()
    {
        questButton.onClick.AddListener(UpdgradeQuest); 
    }
    public override void OnDisable()
    {
        questButton.onClick.RemoveListener(UpdgradeQuest);

    }

    void UpdgradeQuest()
    {
         actualQuest = 0;
            foreach (var item in DataManager.instance.buildings.buildings)
            {
                if (item.buildingName == "DanceFloor")
                {
                actualQuest++;
                }
                if (item.buildingName == "Football")
                {

                    actualQuest++;


                photonView.RPC("ActivateBear", RpcTarget.All);
                    
                }
            }
            if (actualQuest <= 1)
            {
                tmp_title.text = QuestInfo[actualQuest].QuestName;
                tmp_Description.text = QuestInfo[actualQuest].QuestDescription;
            }
            else
            {
                tmp_title.text = "";
                tmp_Description.text = "No hay eventos disponibles";

            }
        
        
     
    }


    [PunRPC]
    public void ActivateBear()
    {
        bear.SetActive(true);

    }
}
