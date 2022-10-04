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
    public Button pickBtn;
    private string role;
    public TMP_Text roleText;
    public GameObject sideBar;
    public TMP_Text btn_description;
    public Slider slider;

    public GameObject builderUI;
    public GameObject chefUI;
    public GameObject watchmenUI;
    public GameObject gathererUI;


    [Header("Message")]
    public GameObject messageScreen;
    public TMP_Text message;
    public TMP_Text caption;
    Coroutine fade;
    public GameObject emoticonBtn;
    public GameObject resourcesBtn;
    public GameObject repairHelper;
    [Header("Resources")]
    public TMP_Text meatAmount;
    public TMP_Text hamburguersAmount;
    public TMP_Text soupAmount;
    public TMP_Text sandwichesAmount;
    public TMP_Text stoneAmount;
    public TMP_Text woodAmount;
    public TMP_Text fabricAmount;

    public TMP_Text connectionAmount;


    [Header("Builder")]
    public GameObject storeButton;
    public GameObject buildingsUI;
    public Button cancel;
    public Button rotate;
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
            case "Vigilante":
                watchmenUI.SetActive(true);
                break;
            case "Constructor":
                builderUI.SetActive(true);
                break;
            case "Recolector":
                gathererUI.SetActive(true);
                break;
        }
    }

    public void StartBuilding()
    {
        sideBar.SetActive(false);
        cancel.gameObject.SetActive(true);
        rotate.gameObject.SetActive(true);
        btn_description.text = "Construir";
        DataManager.instance.gridHexagones.SetActive(true);
    }
    public void StopBuilding()
    {
        sideBar.SetActive(true);
        storeButton.SetActive(true);
        btn_description.text = "";
        cancel.gameObject.SetActive(false);
        rotate.gameObject.SetActive(false);
        DataManager.instance.gridHexagones.SetActive(false);

    }

    public void ChangeResources(Resource resource)
    {

        meatAmount.text = resource.meat.ToString();
        stoneAmount.text =  resource.stone.ToString();
        woodAmount.text =  resource.wood.ToString();
        fabricAmount.text = resource.fabric.ToString();
        connectionAmount.text =  resource.connection.ToString();
        hamburguersAmount.text = resource.food.hamburguer.ToString();
        sandwichesAmount.text =  resource.food.sandwich.ToString();
        soupAmount.text = resource.food.soup.ToString();

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
