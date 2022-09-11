using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcAgent : MonoBehaviour
{
    [SerializeField] TMP_Text m_Text;
    public TutorialAction[] tutorialAction;
    public static NpcAgent instance;
    public GameObject[] interactables;
    public GameObject basket;
    public GameObject store;
    public GameObject[] interactablesGatherer;
    public GameObject tutorial;
    public GameObject map;
    private void Awake()
    {
        instance = this;
    }
     int index = 0;
    private void Start()
    {
        tutorialAction[index].Init();
        map.SetActive(false);
    }
    private void Update()
    {

        m_Text.text = tutorialAction[index].GetDescription();
    }
    public void Next()
    {

        index++;
        tutorialAction[index].Init();
    }

    public void EndTutorial()
    {
        
        tutorial.gameObject.SetActive(false );
        UIController.instance.storeButton.SetActive(false);
        map.SetActive(true);
    }
}
