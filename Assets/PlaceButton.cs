using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaceButton : MonoBehaviour
{
    public PlaceInfo PlaceInfo;
    public GameObject m_resource;
    public TMP_Text buildingName;
    public Button buildButton;

    private TMP_Text stone;
    private TMP_Text wood;
    private TMP_Text fabric;
    private TMP_Text sandwich;
    private TMP_Text hamburguer;
    private TMP_Text soup;
    private TMP_Text interaction;
    private bool firstCheck=true;

    Resource resource;
    Food food;
    private void Awake()
    {
        buildButton.interactable = false;
    }
    private void OnEnable()
    {
        Check();

        if (firstCheck)
        {
            GetInfo();
        }
    }
    private void GetInfo()
    {
        buildingName.text = PlaceInfo.placeName;
         food = new Food(PlaceInfo.hamburguers,PlaceInfo.sandwiches,PlaceInfo.soups);
      
         resource= new Resource(PlaceInfo.stone, PlaceInfo.fabric, PlaceInfo.wood, food);
        resource.connection = PlaceInfo.conexion;
        if (resource.stone > 0)
        {
            GameObject instance = Instantiate(m_resource, m_resource.transform.parent);
            instance.transform.GetChild(0).GetComponent<TMP_Text>().text = "Piedra";
            stone = instance.transform.GetChild(1).GetComponent<TMP_Text>();
            stone.text = resource.stone.ToString();

            
        }


        if (resource.wood > 0)
        {
            GameObject instance = Instantiate(m_resource, m_resource.transform.parent);
            instance.transform.GetChild(0).GetComponent<TMP_Text>().text = "Madera";

            wood = instance.transform.GetChild(1).GetComponent<TMP_Text>();
            wood.text = resource.wood.ToString();
        }

        if (resource.fabric > 0)
        {
            GameObject instance = Instantiate(m_resource, m_resource.transform.parent);
            instance.transform.GetChild(0).GetComponent<TMP_Text>().text="Tela";
            fabric = instance.transform.GetChild(1).GetComponent<TMP_Text>();
            fabric.text = resource.fabric.ToString();

        }
        if (resource.connection > 0)
        {
            GameObject instance = Instantiate(m_resource, m_resource.transform.parent);
            instance.transform.GetChild(0).GetComponent<TMP_Text>().text = "Interacción";
            interaction = instance.transform.GetChild(1).GetComponent<TMP_Text>();
            interaction.text = resource.connection.ToString();

        }
        if (food.sandwich> 0)
        {
            GameObject instance = Instantiate(m_resource, m_resource.transform.parent);
            instance.transform.GetChild(0).GetComponent<TMP_Text>().text = "Sandwich";

            sandwich = instance.transform.GetChild(1).GetComponent<TMP_Text>();
            sandwich.text = food.sandwich.ToString();
        }
        if (food.hamburguer > 0)
        {
            GameObject instance = Instantiate(m_resource, m_resource.transform.parent);
            instance.transform.GetChild(0).GetComponent<TMP_Text>().text = "Hamburguesa";

            hamburguer = instance.transform.GetChild(1).GetComponent<TMP_Text>();
            hamburguer.text = food.hamburguer.ToString();
        }
        if (food.soup > 0)
        {
            GameObject instance = Instantiate(m_resource, m_resource.transform.parent);
            instance.transform.GetChild(0).GetComponent<TMP_Text>().text = "Sopa";

            soup = instance.transform.GetChild(1).GetComponent<TMP_Text>();
            soup.text = food.soup.ToString();
        }

        Destroy(m_resource);
        firstCheck = false;
        Check();

    }

    public void Check()
    {
        buildButton.interactable = true;

        if (!firstCheck)
        {
            Resource gameResources = DataManager.instance.resource;
            if (stone != null)
            {
                if (gameResources.stone >= resource.stone)
                {
                    stone.color = Color.green;
                }
                else
                {
                    stone.color = Color.red;
                    buildButton.interactable = false;

                }

            }

            if (wood != null)
            {
                if (gameResources.wood >= resource.wood)
                {
                    wood.color = Color.green;

                }
                else
                {
                    buildButton.interactable = false;
                    wood.color = Color.red;
                }
            }

            if (fabric != null)
            {
                if (gameResources.fabric >= resource.fabric)
                {
                    fabric.color = Color.green;

                }
                else
                {
                    fabric.color = Color.red;
                    buildButton.interactable = false;

                }
            }
          
            if (hamburguer != null)
            {
                if (gameResources.food.hamburguer >= food.hamburguer)
                {
                    hamburguer.color = Color.green;

                }
                else
                {
                    hamburguer.color = Color.red;
                    buildButton.interactable = false;

                }
            }
            if (soup != null)
            {
                if (gameResources.food.soup >= food.soup)
                {
                    soup.color = Color.green;

                }
                else
                {
                    soup.color = Color.red;
                    buildButton.interactable = false;

                }
            }
            if (sandwich != null)
            {
                if (gameResources.food.sandwich >= food.sandwich)
                {
                    sandwich.color = Color.green;

                }
                else
                {
                    sandwich.color = Color.red;
                                        buildButton.interactable = false;

                }
            }
            if (interaction != null)
            {

                if (gameResources.connection>= resource.connection)
                {
                    interaction.color = Color.green;

                }
                else
                {
                    interaction.color = Color.red;
                    buildButton.interactable = false;

                }
            }
        }
       
        

    }
}
