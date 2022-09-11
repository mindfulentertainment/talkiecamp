using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderMechanics : TutorialAction
{
    public override void Init()
    {
       NpcAgent.instance.store.gameObject.SetActive(true);
        StartCoroutine(End());
    }
    
    IEnumerator End()
    {
        yield return new WaitForSeconds(6);
        textIndex++;
       
        bool hasTent = false;
        while (!hasTent)
        {
            foreach (var item in DataManager.instance.buildings.buildings)
            {
                Debug.Log(item.buildingName);
                if (item.buildingName == "Tent")
                {

                    hasTent = true;
                }
            }

            yield return null;
        }
        textIndex++;
        yield return new WaitForSeconds(10);
        textIndex++;
        yield return new WaitForSeconds(10);

        NpcAgent.instance.EndTutorial();
    }

    public override void Tick()
    {
    }
}
