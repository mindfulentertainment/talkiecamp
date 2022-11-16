using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Ingredient : SnapZone, IPickable
{
    [SerializeField] public IngredientData data;
    private Rigidbody rb;
    private Collider collider;
    bool isTaken = false;

    bool IPickable.isTaken => isTaken;
    public IngredientType Type => data.type;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public void Pick()
    {
        collider.enabled = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void Drop(Vector3 pos)
    {
        gameObject.transform.SetParent(null);
        transform.position = pos;
        collider.enabled = true;
    }

 

    public override bool TryToDropIntoSlot(IPickable pickableToDrop)
    {
        // Ingredients normally don't get any pickables dropped into it.
        // Debug.Log("[Ingredient] TryToDrop into an Ingredient isn't possible by design");
        return false;
    }

    public override IPickable TryToPickUpFromSlot(IPickable playerHoldPickable)
    {
        // Debug.Log($"[Ingredient] Trying to PickUp {gameObject.name}");
        rb.isKinematic = true;
        return this;
    }
}
