using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererMechanics : TutorialAction
{


    public override void Init()
    {
        foreach (var item in NpcAgent.instance.interactablesGatherer)
        {
            item.SetActive(true);
        }
        StartCoroutine(End());
    }

    IEnumerator End()
    {

        yield return new WaitUntil(CheckObjects);
        textIndex++;
        yield return new WaitForSeconds(10);

        NpcAgent.instance.Next();
    }
    bool CheckObjects()
    {
        foreach (var item in NpcAgent.instance.interactablesGatherer)
        {
            if (item.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    public override void Tick()
    {
    }
}
