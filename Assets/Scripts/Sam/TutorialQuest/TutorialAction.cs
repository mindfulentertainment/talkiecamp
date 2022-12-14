using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class TutorialAction : MonoBehaviour
{
    public abstract void Tick();
    public abstract void Init();
    public  TutorialQuest m_Quest;
    protected int textIndex;
    public string GetDescription()
    {
        return m_Quest.tutorialDescription[textIndex];
    }
}
