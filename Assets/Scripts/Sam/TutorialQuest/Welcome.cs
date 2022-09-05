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
        yield return new WaitForSeconds(9.8f);
        textIndex++;
        UIController.instance.roleText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        textIndex++;
        UIController.instance.joystick.gameObject.SetActive(true);
        yield return new WaitForSeconds(10f);
        textIndex++;
        UIController.instance.pickBtn.gameObject.SetActive(true);
        foreach (var item in NpcAgent.instance.interactables)
        {
            item.gameObject.SetActive(true);
        }
        NpcAgent.instance.basket.SetActive(true);
        yield return new WaitUntil(CheckObjects);
        textIndex++;
        yield return new WaitForSeconds(3f);
        textIndex++;
        UIController.instance.resourcesBtn.gameObject.SetActive(true);
        yield return new WaitForSeconds(12f);
        textIndex++;
        UIController.instance.emoticonBtn.gameObject.SetActive(true);
        yield return new WaitForSeconds(10f);
        textIndex++;
        yield return new WaitForSeconds(10f);
        Element element = new Element(Element.ElementType.connection, 10);
        DataManager.instance.IncreaseElement(element);  
        NpcAgent.instance.Next();
    }


    bool CheckObjects()
    {
        foreach (var item in NpcAgent.instance.interactables)
        {
            if(item.gameObject != null)
            {
                return false;
            }
        }
        return true;
    }
}
