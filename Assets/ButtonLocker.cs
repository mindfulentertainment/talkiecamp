using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLocker : MonoBehaviour
{
  
    public void CoolDownButton(Button btn)
    {
        StartCoroutine(CoolDown(btn));
    }

    IEnumerator CoolDown(Button button)
    {
        button.interactable = false;
        yield return new WaitForSeconds(0.19f);
        
            button.interactable = true;
        

    }
}
