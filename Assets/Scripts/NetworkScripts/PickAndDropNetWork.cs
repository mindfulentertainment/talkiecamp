using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PickAndDropNetWork : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform playerPivot;
    [SerializeField] private Transform slot;
    private PickableItem currentPickable;
    private readonly HashSet<PickableItem> pickables = new HashSet<PickableItem>();

    private void Awake()
    {
        if (playerPivot == null)
        {
            playerPivot = transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PickableItem pickedItem = other.gameObject.GetComponent<PickableItem>();

        if (!pickedItem)
        {
            return;
        }

        if (pickables.Contains(pickedItem))
        {
            Debug.LogWarning($"[PickAndDrop] TriggerEnter on a preexisting collider {other.gameObject.name}");
            return;
        }

        pickables.Add(pickedItem);
    }

    private void OnTriggerExit(Collider other)
    {
        PickableItem pickedItem = other.GetComponent<PickableItem>();
        if (pickedItem)
        {
            pickables.Remove(pickedItem);
        }
    }

    private void Update()
    {
      
        if (Input.GetButtonDown("Jump"))
        {
            if (photonView.IsMine)
            {
                photonView.RPC("CheckPickUp", RpcTarget.All);
            }
              
        }
    }

    [PunRPC]
    public void CheckPickUp()
    {
        PickableItem closeItem = TryGetClosestInteractable();

        if (currentPickable == null)
        {
            currentPickable = closeItem;
            if (currentPickable != null)
            {
                PickItem(currentPickable);
                pickables.Remove(currentPickable);
                return;
            }

            return;
        }

        if (closeItem == null || closeItem is PickableItem)
        {
            DropItem(currentPickable);
            currentPickable = null;
            return;
        }
    }
    private void PickItem(PickableItem item)
    {
        //item.Rb.isKinematic = true;
        //item.Rb.velocity = Vector3.zero;
        //item.Rb.angularVelocity = Vector3.zero;

        item.transform.SetParent(slot);

        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
    }

    private void DropItem(PickableItem item)
    {
        item.transform.SetParent(null);

        //item.Rb.isKinematic = false;

        //item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
    }

    private PickableItem TryGetClosestInteractable()
    {
        var minDistance = float.MaxValue;
        PickableItem closest = null;
        foreach (var pickedItem in pickables)
        {
            var distance = Vector3.Distance(playerPivot.position, pickedItem.gameObject.transform.position);
            if (distance > minDistance) continue;
            minDistance = distance;
            closest = pickedItem;
        }

        return closest;
    }
}
