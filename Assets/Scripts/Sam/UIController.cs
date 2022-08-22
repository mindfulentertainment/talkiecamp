using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Joystick joystick;
    private string role;
    public TMP_Text roleText;

    public GameObject builderUI;
    public GameObject chefUI;
    public GameObject watchmenUI;
    public GameObject gathererUI;

    [Header("Builder")]
    public GameObject storeButton;
    public GameObject buildingsUI;

    private void Awake()
    {
        instance = this;
    }
   void Start()
    {
        role = PlayerPrefs.GetString("role");
        roleText.text = role;
        switch (role)
        {
            case "Chef":
                chefUI.SetActive(true);
                break;
            case "Watchmen":
                watchmenUI.SetActive(true);
                break;
            case "Builder":
                builderUI.SetActive(true);
                break;
            case "Gatherer":
                gathererUI.SetActive(true);
                break;
        }
    }
}
