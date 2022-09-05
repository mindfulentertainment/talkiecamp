using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DishTray : SnapZone
{
    private readonly List<Plate> _plates = new List<Plate>();

    public override bool TryToDropIntoSlot(IPickable pickableToDrop) => false;

    public override IPickable TryToPickUpFromSlot(IPickable playerHoldPickable)
    {
        if (playerHoldPickable != null) return null;
        if (_plates.Count == 0) return null;

        var bottomPlate = _plates[0];
        _plates.Clear();
        return bottomPlate;
    }

    public void AddPlate(Plate plate)
    {
        var topPileSlot = _plates.Count == 0 ? Slot : _plates.Last().Slot;

        // plates are parented to the Slot of the previous one
        plate.transform.SetParent(topPileSlot);
        plate.transform.SetPositionAndRotation(topPileSlot.transform.position, Quaternion.identity);
        _plates.Add(plate);
    }
}
