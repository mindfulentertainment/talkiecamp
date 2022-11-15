using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Plate : SnapZone, IPickable
{
    public const int MaxNumberIngredients = 4;
    bool isTaken = false;

    bool IPickable.isTaken => isTaken;
    private Rigidbody rb;
    private Collider collider;
    private readonly List<Ingredient> _ingredients = new List<Ingredient>(MaxNumberIngredients);

    public List<Ingredient> Ingredients => _ingredients;
    public bool IsEmpty() => _ingredients.Count == 0;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

#if UNITY_EDITOR
        Assert.IsNotNull(rb);
        Assert.IsNotNull(collider);
#endif
    }

    public bool AddIngredients(List<Ingredient> ingredients)
    {
        if (!IsEmpty()) return false;
        _ingredients.AddRange(ingredients);

        foreach (var ingredient in _ingredients)
        {
            ingredient.transform.SetParent(Slot);
            ingredient.transform.SetPositionAndRotation(Slot.transform.position, Quaternion.identity);
        }

        return true;
    }

    public void RemoveAllIngredients()
    {
        DisableIngredients();
        _ingredients.Clear();
    }

    private void DisableIngredients()
    {
        foreach (var item in _ingredients)
        {
            Destroy(item.gameObject, 2f);
        }
    }

    public void Pick()
    {
        collider.enabled = false;
    }

    public void Drop(Vector3 pos)
    {
        gameObject.transform.SetParent(null);
        transform.position = pos;
        collider.enabled = true;
    }

    public override bool TryToDropIntoSlot(IPickable pickableToDrop)
    {
        if (pickableToDrop == null) return false;

        switch (pickableToDrop)
        {
            case Ingredient ingredient:
                if (ingredient.Type == IngredientType.Bun ||
                    ingredient.Type == IngredientType.Tomato ||
                    ingredient.Type == IngredientType.Lettuce ||
                    ingredient.Type == IngredientType.Ham || 
                    ingredient.Type == IngredientType.Meat || 
                    ingredient.Type == IngredientType.Carrot)
                {
                    return TryDrop(pickableToDrop);
                }
                return false;
            case Plate plate:
                //Debug.Log("[Plate] Trying to drop something from a plate into other plate! We basically swap contents");
                if (this.IsEmpty() == false) return false;
                this.AddIngredients(plate.Ingredients);
                plate.RemoveAllIngredients();
                return false;
            default:
                Debug.LogWarning("[Plate] Drop not recognized", this);
                break;
        } 
        return false;
    }

    public override IPickable TryToPickUpFromSlot(IPickable playerHoldPickable)
    {
        if (playerHoldPickable == null) return null;

        switch (playerHoldPickable)
        {
            case Ingredient ingredient:
                //TODO: we can pickup some ingredients into plate, not all of them.
                break;
            // swap plate ingredients
            case Plate plate:
                if (plate.IsEmpty())
                {
                    if (this.IsEmpty()) return null;
                    plate.AddIngredients(this._ingredients);
                }
                break;
        }
        return null;
    }

    private bool TryDrop(IPickable pickable)
    {
        if (Ingredients.Count >= MaxNumberIngredients) return false;

        var ingredient = pickable as Ingredient;
        if (ingredient == null)
        {
            Debug.LogWarning("[CookingPot] Can only drop ingredients into this object", this);
            return false;
        }

        Ingredients.Add(ingredient);

        ingredient.gameObject.transform.SetParent(Slot);
        ingredient.gameObject.transform.SetPositionAndRotation(Slot.position, Quaternion.identity);

        //UpdateIngredientsUI();
        return true;
    }
}
