using UnityEngine;

public class Table : SnapZone
{
    public override bool TryToDropIntoSlot(IPickable pickableToDrop)
    {
        if (CurrentPickable == null) return TryDropIfNotOccupied(pickableToDrop);

        return false;
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
        if (CurrentPickable != null) return false;

        CurrentPickable = pickable;
        CurrentPickable.gameObject.transform.SetParent(Slot);
        CurrentPickable.gameObject.transform.SetPositionAndRotation(Slot.position, Quaternion.identity);
        return true;
    }
}
