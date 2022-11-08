using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
public class PlantSeed : Interactable
{
    [SerializeField] private GameObject seedPanel;
    [SerializeField] private GameObject[] plants;
    private int lechuga, cebolla, tomate, trigo;
    Coroutine coroutine;
    public static bool IsInteractable=true;
    private void Start()
    {

        seedPanel = UIController.instance.seedPanel;
    }

    private void Update()
    {
        cebolla = PlayerPrefs.GetInt("onion");
        tomate = PlayerPrefs.GetInt("tomato");
        lechuga = PlayerPrefs.GetInt("lettuce");
        trigo = PlayerPrefs.GetInt("wheat");

    }
    private void OnMouseDown()
    {
        if (IsInteractable)
        {
            CreatePlants.seed = this;
        }
        
    }
    public override void OnInteraction()
    {
        seedPanel.transform.GetChild(0).gameObject.SetActive(true);
        IsInteractable = false;
    }

    public void MakeInteractable()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine=StartCoroutine(InteractableCoroutine());
    }

    IEnumerator InteractableCoroutine()
    {
        yield return new WaitForSeconds(1);
        IsInteractable=true;
    }

    public void Plant(int num)
    {
        MakeInteractable();
        if (num == 1 && cebolla > 0)
        {
           
            cebolla = cebolla - 1;
            PlayerPrefs.SetInt("onion", cebolla);
            CreatePlant(plants[0]);
            PlayerPrefs.SetInt("actualPlant", num);

        }
         
       
        
        if (num == 2&&tomate>0)
        {
           
            tomate = tomate - 1;
            PlayerPrefs.SetInt("tomato", tomate);
            CreatePlant(plants[1]);
            PlayerPrefs.SetInt("actualPlant", num);
        }
        
       
        if (num == 3&& lechuga>0)
        {
            
            lechuga = lechuga - 1;
            PlayerPrefs.SetInt("lettuce", lechuga);
            CreatePlant(plants[2]);
            PlayerPrefs.SetInt("actualPlant", num);
        }
        
       
        if (num == 4&&trigo>0)
        {
           
            trigo = trigo - 1;
            PlayerPrefs.SetInt("wheat", trigo);
            CreatePlant(plants[3]);
            PlayerPrefs.SetInt("actualPlant", num);
        }

       // CreatePlant(plants[1]);
    }


    public override void CreatePlant(GameObject plant)
    {
        Debug.Log(gameObject.name);
        var newplant = PhotonNetwork.Instantiate(plant.name, this.transform.position, Quaternion.identity);
        newplant.transform.parent = gameObject.transform;
        gameObject.GetComponent<BoxCollider>().enabled = false;

    }

}
