using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class BearTrap : SnapZone, IPickable
{
    private Rigidbody rb;
    private Collider collider;
    bool isTaken = false;

    BearController bearController;

    bool IPickable.isTaken => isTaken;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bear"))
        {
            bearController = other.GetComponent<BearController>();

            bearController.CaughtInTrap();
        }
    }

    public void Pick()
    {
        if (!isTaken)
        {
            collider.enabled = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            isTaken = true;
            GetComponent<PhotonTransformView>().enabled = false;
            StopAllCoroutines();
        }
    }

    public void Drop(Vector3 pos)
    {
        isTaken = false;

        gameObject.transform.SetParent(null);
        transform.position = pos;
        GetComponent<PhotonTransformView>().enabled = true;
        collider.enabled = true;
    }

    public override bool TryToDropIntoSlot(IPickable pickableToDrop)
    {
        return false;
    }

    public override IPickable TryToPickUpFromSlot(IPickable playerHoldPickable)
    {
        return this;
    }
}
