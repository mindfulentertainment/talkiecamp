using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PickableItem : SnapZone, IPickable
{
    private Rigidbody rb;
    private Collider collider;
    bool isTaken=false;

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

    public void Pick()
    {
        if (!isTaken)
        {
            if (GetComponent<PhotonRigidbodyView>() != null)
            {
                GetComponent<PhotonRigidbodyView>().enabled= false;
                GetComponent<PhotonView>().FindObservables();    //UIController.instance.pickBtn.GetComponent<Image>().sprite = ballInteractionIcon;    //cambia el icono de Ui de interaccion
            }

            rb.isKinematic = true;
            collider.enabled = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            isTaken = true;
            //GetComponent<PhotonTransformView>().enabled = false;

            StopAllCoroutines();
        }
          
        
     
    }

    public void Drop(Vector3 pos)
    {
       
            isTaken = false;

            gameObject.transform.SetParent(null);
            transform.position = pos;
            //GetComponent<PhotonTransformView>().enabled = true;
            rb.isKinematic = true;
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
