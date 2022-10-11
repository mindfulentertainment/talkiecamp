using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    private OrderData _orderData;

    public bool IsDelivered { get; private set; }

    public OrderData OrderData => _orderData;
    public List<IngredientData> Ingredients => _orderData.ingredients;
    public string OrderName => _orderData.orderName;

    public delegate void Delivered(Order order);
    public event Delivered OnDelivered;

    public void Setup(OrderData orderData)
    {
        IsDelivered = false;
        _orderData = orderData;
    }

    public void SetOrderDelivered()
    {
        IsDelivered = true;
    }
}
