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
    public GameObject floor;
    public GameObject[] interactablesGatherer;
    public GameObject tutorial;

    private void Awake()
    {
        instance = this;
    }
     int index = 0;
    private void Start()
    {
        tutorialAction[index].Init();
        UIController.instance.roleText.gameObject.SetActive(false);
        UIController.instance.pickBtn.gameObject.SetActive(false);
        UIController.instance.joystick.gameObject.SetActive(false);
        UIController.instance.emoticonBtn.gameObject.SetActive(false);
        UIController.instance.resourcesBtn.gameObject.SetActive(false);
        floor.GetComponent<Collider>().enabled = false;
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
        floor.GetComponent<Collider>().enabled = true;

        tutorial.gameObject.SetActive(false );
    }
}
