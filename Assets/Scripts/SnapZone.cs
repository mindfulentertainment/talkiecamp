using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

[RequireComponent(typeof(Collider))]
public abstract class SnapZone : MonoBehaviour
{
    [Tooltip("Pivot where IPickables could be dropped/pickedUp")]
    [SerializeField] protected Transform slot;

    protected IPickable CurrentPickable { get; set; }

    public Transform Slot => slot;

    protected virtual void Awake()
    {
        CheckSlotOccupied();
    }

    private void CheckSlotOccupied()
    {
        if (Slot == null) return;
        foreach (Transform child in Slot)
        {
            CurrentPickable = child.GetComponent<IPickable>();
            if (CurrentPickable != null) return;
        }
    }

    public abstract bool TryToDropIntoSlot(IPickable pickableToDrop);
    [CanBeNull] public abstract IPickable TryToPickUpFromSlot(IPickable playerHoldPickable);
}
