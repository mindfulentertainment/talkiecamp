using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(OrderManager))]
public class CookingManager : MonoBehaviour
{
    private static CookingManager _instance;

    public static CookingManager Instance
    {
        get
        {
            if ((Object)_instance != (Object)null)
            {
                return _instance;
            }

            CookingManager[] array = Object.FindObjectsOfType(typeof(CookingManager)) as CookingManager[];
            if (array != null && array.Length != 0)
            {
                _instance = array[0];
            }

            if (array != null && array.Length > 1)
            {
                Debug.LogError("[Singleton] There is more than one " + typeof(CookingManager).Name + " in the scene.");
            }

            if ((Object)_instance != (Object)null)
            {
                return _instance;
            }

            Debug.LogWarning("[Singleton] There is no instance of " + typeof(CookingManager).Name + " in the scene. Creating one now");
            _instance = new GameObject
            {
                name = "_" + typeof(CookingManager).Name
            }.AddComponent<CookingManager>();
            return _instance;
        }
    }

    [SerializeField] private DishTray dishTray;
    [SerializeField] private OrderManager orderManager;
    //[SerializeField] private MealData mealData; No necesario actualmente, se puede ultilizar cuando se requiera crear eventos con diferentes tipos de comidas

    private const float TimeToReturnPlateSeconds = 3f;
    private readonly WaitForSeconds _timeToReturnPlate = new WaitForSeconds(TimeToReturnPlateSeconds);

    private void Awake()
    {
#if UNITY_EDITOR
        Assert.IsNotNull(dishTray);
        Assert.IsNotNull(orderManager);
#endif
    }

    private void OnEnable()
    {
        DeliverTable.OnPlateDropped += HandlePlateDropped;
    }

    private void OnDisable()
    {
        DeliverTable.OnPlateDropped -= HandlePlateDropped;
    }

    private void HandlePlateDropped(Plate plate)
    {
        if (plate.IsEmpty())
        {
            plate.RemoveAllIngredients();
            StartCoroutine(ReturnPlate(plate));
            return;
        }

        orderManager.CheckIngredientsMatchOrder(plate.Ingredients);
        plate.RemoveAllIngredients();
        StartCoroutine(ReturnPlate(plate));
    }

    private IEnumerator ReturnPlate(Plate plate)
    {
        plate.gameObject.SetActive(true);
        yield return _timeToReturnPlate;
        dishTray.AddPlate(plate);
    }
}
