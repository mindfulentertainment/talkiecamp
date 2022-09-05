using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderMechanics : TutorialAction
{
    public override void Init()
    {
       NpcAgent.instance.store.gameObject.SetActive(true);
        StartCoroutine(End());
        Place.OnPlaceBuild += Dance;
    }
    
    IEnumerator End()
    {
        yield return new WaitForSeconds(6);
        textIndex++;
       
    }
    IEnumerator ContinueToDance()
    {
        textIndex++;
        yield return new WaitForSeconds(20);
        textIndex++;
        yield return new WaitForSeconds(10);
        NpcAgent.instance.EndTutorial();

    }
    void Dance(string building)
    {
        if(building == "DanceFloor")
        {
            StartCoroutine(ContinueToDance());
            Place.OnPlaceBuild -= Dance;

        }
    }
    public override void Tick()
    {
    }
}
