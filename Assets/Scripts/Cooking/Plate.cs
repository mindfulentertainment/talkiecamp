using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Plate : SnapZone, IPickable
{
    private const int MaxNumberIngredients = 4;

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
        _ingredients.Clear();
    }

    public void Pick()
    {
        rb.isKinematic = true;
        collider.enabled = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void Drop()
    {
        gameObject.transform.SetParent(null);
        rb.isKinematic = false;
        collider.enabled = true;

        StartCoroutine(KinematicDisable());
    }

    IEnumerator KinematicDisable()
    {
        yield return new WaitForSeconds(1.5f);

        rb.isKinematic = true;
    }

    public override bool TryToDropIntoSlot(IPickable pickableToDrop)
    {
        if (pickableToDrop == null) return false;

        switch (pickableToDrop)
        {
            case Ingredient ingredient:
                Debug.Log("[Plate] Trying to dropping Ingredient into Plate! Not implemented");
                break;
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
        // We can pickup Ingredients from plates with other plates (effectively swapping content) or from Pans

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
}
