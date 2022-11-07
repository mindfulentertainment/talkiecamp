using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : SnapZone
{
    public override bool TryToDropIntoSlot(IPickable pickable)
    {
        if (pickable == null|| pickable.gameObject.GetComponent<Element>()==null)
        {
            return false;
        }
        else
        {
            CurrentPickable = pickable;
            Element element;
            if (CurrentPickable.gameObject != null)
            {
                CurrentPickable.gameObject.TryGetComponent(out element);
               
                DataManager.instance.IncreaseElement(element);

                CurrentPickable.gameObject.transform.SetParent(Slot);
                CurrentPickable.gameObject.transform.SetPositionAndRotation(Slot.position, Quaternion.identity);
                CurrentPickable.gameObject.SetActive(false);
                GameObject toDestroy = CurrentPickable.gameObject;
                CurrentPickable = null;
                Destroy(toDestroy.gameObject, 2f);
            }
            return true;


        }



    }

    public override IPickable TryToPickUpFromSlot(IPickable playerHoldPickable)
    {
        if (CurrentPickable == null) return null;

        var output = CurrentPickable;
        CurrentPickable = null;
        return output;
    }

    private bool TryDropIfNotOccupied(IPickable pickable)
    {

       

        return false;
    }
}
