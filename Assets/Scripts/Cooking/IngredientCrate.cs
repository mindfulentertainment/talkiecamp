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
        
        return false;
    }

    public override IPickable TryToPickUpFromSlot(IPickable playerHoldPickable)
    {
        if(playerHoldPickable == null)
        {
            if (CurrentPickable == null)
            {
                if (DataManager.instance.CheckResourceAmount(ingredientPrefab.Type.ToString().ToLower()))
                {
                    return Instantiate(ingredientPrefab, Slot.transform.position, Quaternion.identity);
                }
            }
        } 
     

        var output = CurrentPickable;
        StartCoroutine(NullPickUp());
        return output;
    }

    private IEnumerator NullPickUp()
    {
        yield return new WaitForEndOfFrame();
        CurrentPickable = null;
    }
}
