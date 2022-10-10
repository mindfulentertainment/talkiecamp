using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class IngredientCrate : SnapZone
{
    [SerializeField] private Ingredient ingredientPrefab;

    protected override void Awake()
    {
        base.Awake();

#if UNITY_EDITOR
        Assert.IsNotNull(ingredientPrefab);
#endif
    }

    public override bool TryToDropIntoSlot(IPickable pickableToDrop)
    {
        if (CurrentPickable != null) return false;

        CurrentPickable = pickableToDrop;
        CurrentPickable.gameObject.transform.SetParent(Slot);
        pickableToDrop.gameObject.transform.SetPositionAndRotation(Slot.position, Quaternion.identity);
        return true;
    }

    public override IPickable TryToPickUpFromSlot(IPickable playerHoldPickable)
    {
        if (CurrentPickable == null)
        {
            return Instantiate(ingredientPrefab, Slot.transform.position, Quaternion.identity);
        }

        var output = CurrentPickable;
        CurrentPickable = null;
        return output;
    }
}
