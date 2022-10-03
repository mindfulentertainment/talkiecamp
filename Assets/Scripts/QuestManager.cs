using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestManager : MonoBehaviour
{
    public Button questButton;
    public QuestInfo[] QuestInfo;
    public TMP_Text tmp_Description;
    public TMP_Text tmp_title;
   
    public  void OnEnable()
    {
        questButton.onClick.AddListener(UpdgradeQuest);
    }
    public void OnDisable()
    {
        questButton.onClick.RemoveListener(UpdgradeQuest);

    }

    
    void UpdgradeQuest()
    {
        QuestInfo actualQuestInfo=null;

        for (int i = 0; i < QuestInfo.Length; i++)
        {
            if (!CheckConstruction(QuestInfo[i].name))
            {

                actualQuestInfo=QuestInfo[i];
                break;
            }

        }

        if (actualQuestInfo != null)
        {
            tmp_title.text = actualQuestInfo.QuestName;
            tmp_Description.text = actualQuestInfo.QuestDescription;
        }
        else
        {
            tmp_title.text = "";
            tmp_Description.text = "No hay eventos disponibles";

        }
    }

    bool CheckConstruction(string name)
    {
        foreach (var item in DataManager.instance.buildings.buildings)
        {
            if (item.buildingName == name)
            {
                return true;
               
            }
           
        }

        return false;
    }

    
}
