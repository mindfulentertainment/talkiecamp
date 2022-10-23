using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaceOrderButton : MonoBehaviour
{
    [SerializeField] OrderData orderData;
    [SerializeField] TMP_Text orderName;
    [SerializeField] Image foodImage;
    [SerializeField] Sprite foodSprite;
    [SerializeField] Button orderButton;
    [SerializeField] OrderManager orderManager;

    private void Start()
    {
        orderName.text = orderData.orderName;
        foodImage.sprite = foodSprite;
        orderButton.onClick.AddListener(PlaceOrder);
    }

    private void PlaceOrder()
    {
        orderManager.GenerateOrder(orderData);
    }
}
