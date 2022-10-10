using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrderData", menuName = "OrderData", order = 2)]
public class OrderData : ScriptableObject
{
    public string orderName;
    public List<IngredientData> ingredients;
}
