using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TutorialCheck : MonoBehaviour
{
    [SerializeField] GameObject tutorialContent;
   
    public bool showTutorial;
    private void OnEnable()
    {
        MatchManager.OnGameStart += CheckTutorial;
    }
    private void OnDisable()
    {
        MatchManager.OnGameStart -= CheckTutorial;
    }
    void CheckTutorial()
    {
        
     


        if (!CheckTutorialDone())
        {
            if (showTutorial)
            {
                tutorialContent.SetActive(true);

            }

        }

    }


    private bool CheckTutorialDone()
    {
        foreach (var item in  DataManager.instance.buildings.buildings)
        {
           
            if (item.buildingName == "Tent")
            {
                return true;
                
            }
        }
        return false;
    }


}
