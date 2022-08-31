using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcAgent : MonoBehaviour
{
    [SerializeField] TMP_Text m_Text;
    [SerializeField] TutorialQuest[] m_Quest;
    public TutorialAction[] tutorialAction;

    static int index = 0;
    private void Start()
    {
        tutorialAction[index].Init();

    }

    private void Update()
    {

        m_Text.text = m_Quest[index].tutorialDescription;
        tutorialAction[index].Tick();
    }
    public static void Next()
    {

        index++;
    }
}
