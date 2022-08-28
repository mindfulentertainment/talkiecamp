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


    [Header("Message")]
    public GameObject messageScreen;
    public TMP_Text message;
    public TMP_Text caption;
    Coroutine fade;

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
    public void ScreenON()
    {
        LoadingScreen.SetActive(true);

    }
    void ScreenOFF()
    {
        LoadingScreen.SetActive(false);

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

    public void ShowMessage(string message)
    {
        this.message.text = message;
        if (fade != null)
        {
            StopCoroutine(fade);
        }
        
        fade=StartCoroutine(FadeScreen(4, messageScreen));

    }
    IEnumerator FadeScreen(int seconds, GameObject screen)
    {
        screen.SetActive(true);
        yield return new WaitForSeconds(seconds);
        screen.SetActive(false);

    }

    public void ShowCaption(string message)
    {
        caption.text=message;
        caption.gameObject.SetActive(true);
    }

    public void HideCaption()
    {
        caption.gameObject.SetActive(false);

    }
}
