using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public GameObject LoadingScreen;

    public Joystick joystick;
    private string role;
    public TMP_Text roleText;

    public GameObject builderUI;
    public GameObject chefUI;
    public GameObject watchmenUI;
    public GameObject gathererUI;


    [Header("Resources")]
    public TMP_Text meatAmount;
    public TMP_Text stoneAmount;
    public TMP_Text woodAmount;
    public TMP_Text concreteAmount;

    [Header("Builder")]
    public GameObject storeButton;
    public GameObject buildingsUI;

    private void Awake()
    {
        instance = this;
        LoadingScreen.SetActive(true);
    }

    private void OnEnable()
    {
        MatchManager.OnGameStart += ScreenOFF;
    }
    private void OnDisable()
    {
        MatchManager.OnGameStart -= ScreenOFF;

    }

    void ScreenOFF()
    {
        LoadingScreen.SetActive(false);
        MatchManager.OnGameStart -= ScreenOFF;

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

    public void ChangeResources(Resource resource)
    {
        meatAmount.text = "Meat "+ resource.meat.ToString();
        stoneAmount.text = "Stone " + resource.stone.ToString();
        woodAmount.text = "Wood "+ resource.wood.ToString();
        concreteAmount.text = "Concrete " + resource.concrete.ToString();
    }
}
