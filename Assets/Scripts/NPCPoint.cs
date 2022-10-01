using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NPCPoint : MonoBehaviour
{
    public QuestCenter centralQuest;
    public TMP_Text info;
    public GameObject eventButtons;
    public void Enter()
    {
        string randomS = centralQuest.messages[Random.Range(0, centralQuest.messages.Length)];
        info.text = randomS;
        eventButtons.SetActive(true);
    }

    public void Exit()
    {
        info.text = "";
        eventButtons.SetActive(false);

    }
}
