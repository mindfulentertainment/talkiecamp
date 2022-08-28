using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PickableItem : SnapZone, IPickable
{
    private Rigidbody rb;
    private Collider collider;

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
