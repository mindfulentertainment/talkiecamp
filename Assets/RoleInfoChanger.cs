using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleInfoChanger : MonoBehaviour
{
    private void OnEnable()
    {
        string playerType = PlayerPrefs.GetString("role");
        switch (playerType)
        {
            case "Chef":
                transform.GetChild(0).gameObject.SetActive(true);
                break;
            case "Vigilante":
                transform.GetChild(1).gameObject.SetActive(true);
                break;
            case "Recolector":
                transform.GetChild(2).gameObject.SetActive(true);
                break;
            case "Constructor":
                transform.GetChild(3).gameObject.SetActive(true);
                break;

        }
    }
   
}
