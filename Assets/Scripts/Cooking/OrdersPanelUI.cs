using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrdersPanelUI : MonoBehaviour
{
    [Header("Orders")]
    [SerializeField] TMP_Text burger01;
    [SerializeField] TMP_Text burger02;
    [SerializeField] TMP_Text sandwich01;
    [SerializeField] TMP_Text sandwich02;
    [SerializeField] TMP_Text tomatoSoup;
    [SerializeField] TMP_Text carrotSoup;

    private int burger01Amount = 0;
    private int burger02Amount = 0;
    private int sandwich01Amount = 0;
    private int sandwich02Amount = 0;
    private int tomatoSoupAmount = 0;
    private int carrotSoupAmount = 0;

    private void OnEnable()
    {
        OrderManager.OnOrderSpawned += HandleOrderSpawned;
        OrderManager.OnOrderDelivered += HandleOrderDelivered;
    }

    private void OnDisable()
    {
        OrderManager.OnOrderSpawned -= HandleOrderSpawned;
        OrderManager.OnOrderDelivered -= HandleOrderDelivered;
    }

    private void HandleOrderSpawned(Order order)
    {
        switch (order.OrderName)
        {
            case "Hamburguesa":
                burger01Amount++;
                burger01.text = burger01Amount.ToString();
                break;

            case "Hamburguesa Especial":
                burger02Amount++;
                burger02.text = burger02Amount.ToString();
                break;

            case "Sopa Tomate":
                tomatoSoupAmount++;
                tomatoSoup.text = tomatoSoupAmount.ToString();
                break;

            case "Sopa Zanahoria":
                carrotSoupAmount++;
                carrotSoup.text = carrotSoupAmount.ToString();
                break;

            case "Sanduche":
                sandwich01Amount++;
                sandwich01.text = sandwich01Amount.ToString();
                break;

            case "Sanduche Especial":
                sandwich02Amount++;
                sandwich02.text = sandwich02Amount.ToString();
                break;

            default:
                break;
        }
    }

    private void HandleOrderDelivered(Order order)
    {
        UpdateResources(order);
    
        switch (order.OrderName)
        {
            case "Hamburguesa":
                burger01Amount--;
                burger01.text = burger01Amount.ToString();
                break;

            case "Hamburguesa Especial":
                burger02Amount--;
                burger02.text = burger02Amount.ToString();
                break;

            case "Sopa Tomate":
                tomatoSoupAmount--;
                tomatoSoup.text = tomatoSoupAmount.ToString();
                break;

            case "Sopa Zanahoria":
                carrotSoupAmount--;
                carrotSoup.text = carrotSoupAmount.ToString();
                break;

            case "Sanduche":
                sandwich01Amount--;
                sandwich01.text = sandwich01Amount.ToString();
                break;

            case "Sanduche Especial":
                sandwich02Amount--;
                sandwich02.text = sandwich02Amount.ToString();
                break;

            default:
                break;
        }
    }

    private void UpdateResources(Order order)
    {
        switch (order.OrderName)
        {
            case "Hamburguesa":
                Element burguer01 = new Element(Element.ElementType.hamburguer, 1);
                DataManager.instance.IncreaseElement(burguer01);
                break;

            case "Hamburguesa Especial":
                Element burger02 = new Element(Element.ElementType.hamburguer_2, 1);
                DataManager.instance.IncreaseElement(burger02);
                break;

            case "Sopa Tomate":
                Element TomatoSoup = new Element(Element.ElementType.soup, 1);
                DataManager.instance.IncreaseElement(TomatoSoup);
                break;

            case "Sopa Zanahoria":
                Element CarrotSoup = new Element(Element.ElementType.soup_2, 1);
                DataManager.instance.IncreaseElement(CarrotSoup);
                break;

            case "Sanduche":
                Element Sandwich01 = new Element(Element.ElementType.sandwich, 1);
                DataManager.instance.IncreaseElement(Sandwich01); 
                break;

            case "Sanduche Especial":
                Element Sandwich02 = new Element(Element.ElementType.sandwich_2, 1);
                DataManager.instance.IncreaseElement(Sandwich02);
                break;

            default:
                break;
        }
    }
}
