using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MealData", menuName = "MealData", order = 1)]
public class MealData : ScriptableObject
{
    public string mealName;
    [Tooltip("Orders that going to be randomly spawned")]
    public List<OrderData> orders;
}
