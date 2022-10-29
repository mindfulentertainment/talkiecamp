using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private Order orderPrefab;
    [SerializeField] private int maxConcurrentOrders = 5;
    private OrderData currentOrder;

    private readonly List<Order> _orders = new List<Order>();
    private readonly Queue<Order> _poolOrders = new Queue<Order>();

    public delegate void OrderSpawned(Order order);
    public static event OrderSpawned OnOrderSpawned;

    public delegate void OrderDelivered(Order order);
    public static event OrderDelivered OnOrderDelivered;

    private void Awake()
    {
        _orders.Clear();
    }

    private Order GetOrderFromPool()
    {
        return _poolOrders.Count > 0 ? _poolOrders.Dequeue() : Instantiate(orderPrefab, transform);
    }

    public void GenerateOrder(OrderData orderData)
    {
        currentOrder = orderData;
        TrySpawnOrder();
    }

    private void TrySpawnOrder()
    {
        if (_orders.Count < maxConcurrentOrders)
        {
            var order = GetOrderFromPool();
            if (order == null)
            {
                Debug.LogWarning("[OrderManager] Couldn't pick an Order from pool", this);
                return;
            }

            order.Setup(Instantiate(currentOrder));
            _orders.Add(order);
            foreach (var item in order.Ingredients)
            {
                Debug.Log(item.ToString());
            }
            OnOrderSpawned?.Invoke(order);
        }
    }

    private void DeactivateSendBackToPool(Order order)
    {
        order.SetOrderDelivered();
        _orders.RemoveAll(x => x.IsDelivered);
        _poolOrders.Enqueue(order);
    }

    public void CheckIngredientsMatchOrder(List<Ingredient> ingredients)
    {
        if (ingredients == null) return;
        List<IngredientType> plateIngredients = ingredients.Select(x => x.Type).ToList();

        List<Order> orderByArrivalNotDelivered = _orders
            .Where(x => x.IsDelivered == false).ToList();

        for (int i = 0; i < orderByArrivalNotDelivered.Count; i++)
        {
            var order = orderByArrivalNotDelivered[i];

            List<IngredientType> orderIngredients = order.Ingredients.Select(x => x.type).ToList();

            if (plateIngredients.Count != orderIngredients.Count)
            {
                Debug.Log("Orden erronea");

                continue;
            }

            var intersection = plateIngredients.Except(orderIngredients).ToList();

            // doesn't match any plate
            if (intersection.Count != 0)
            {
                Debug.Log("Orden erronea");

                continue;
            }

            DeactivateSendBackToPool(order);
            Debug.Log("Orden correcta");
            OnOrderDelivered?.Invoke(order);
            return;
        }
    }
}
