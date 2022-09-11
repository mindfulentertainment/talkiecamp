using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverTable : SnapZone
{

    public delegate void PlateDropped(Plate plate);
    public static event PlateDropped OnPlateDropped;
    public delegate void PlateMissing();
    public static event PlateMissing OnPlateMissing;

    protected override void Awake()
    {
        base.Awake();
    }

    public override bool TryToDropIntoSlot(IPickable pickableToDrop)
    {
        if (pickableToDrop == null) return false;

        switch (pickableToDrop)
        {
            case Plate plate:
                plate.transform.SetParent(null);
                plate.transform.SetPositionAndRotation(Slot.position, Quaternion.identity);
                OnPlateDropped?.Invoke(plate);
                // move the plate out-of sight
                plate.transform.position = new Vector3(10000f, 10000f, 10000f);
                return true;
            case Ingredient ingredient:
                OnPlateMissing?.Invoke();
                return false;
        }
        return false;
    }

    public override IPickable TryToPickUpFromSlot(IPickable playerHoldPickable) => null;
}
