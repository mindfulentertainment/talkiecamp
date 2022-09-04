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
    public GameObject resourcesBar;
    public GameObject sideBar;
    public TMP_Text btn_description;

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

    public void StartBuilding()
    {
        resourcesBar.SetActive(false); 
        sideBar.SetActive(false);
        cancel.gameObject.SetActive(true);
        rotate.gameObject.SetActive(true);
        btn_description.text = "Construir";
    }
    public void StopBuilding()
    {
        resourcesBar.SetActive(true);
        sideBar.SetActive(true);
        storeButton.SetActive(true);
        btn_description.text = "";
        cancel.gameObject.SetActive(false);
        rotate.gameObject.SetActive(false);
    }

    public void ChangeResources(Resource resource)
    {

        meatAmount.text = "Carne "+ resource.meat.ToString();
        stoneAmount.text = "Piedra " + resource.stone.ToString();
        woodAmount.text = "Madera "+ resource.wood.ToString();
        fabricAmount.text = "Tela " + resource.fabric.ToString();
        connectionAmount.text = "Interacción " + resource.connection.ToString();
        hamburguersAmount.text = "Hamburguesas " + resource.food.hamburguer.ToString();
        sandwichesAmount.text = "Sánduches " + resource.food.sandwich.ToString();
        soupAmount.text = "Sopas " + resource.food.soup.ToString();

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
