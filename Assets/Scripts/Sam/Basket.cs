using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : SnapZone
{
    public override bool TryToDropIntoSlot(IPickable pickable)
    {
        if (pickable == null)
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
                if (element != null)
                {
                    switch (element.type)
                    {
                        case Element.ElementType.wood:
                            DataManager.instance.IncreaseWood(element.GetAmount());
                            break;

                        case Element.ElementType.stone:
                            break;
                    }

                }
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

       

        return true;
    }
}
