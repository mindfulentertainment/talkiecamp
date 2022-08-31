using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Welcome : TutorialAction
{
    public override void Init()
    {
        Debug.Log("Started");
        StartCoroutine(End());
    }
    public override void Tick()
    {

    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(15);
        NpcAgent.Next();
    }
}
