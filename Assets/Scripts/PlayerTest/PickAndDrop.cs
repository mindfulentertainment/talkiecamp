using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

public class PickAndDrop : MonoBehaviour
{
    [SerializeField] private Transform playerPivot;
    private readonly HashSet<SnapZone> snapZones = new HashSet<SnapZone>();

    private void Awake()
    {
        if (playerPivot == null)
        {
            playerPivot = transform;
        }
    }

    [CanBeNull]
    public SnapZone CurrentSnapZone { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        SnapZone snapZone = other.gameObject.GetComponent<SnapZone>();
        if (!snapZone) return;

        if (snapZones.Contains(snapZone))
        {
            Debug.LogWarning($"[InteractableController] TriggerEnter on a preexisting collider {other.gameObject.name}");
            return;
        }
        snapZones.Add(snapZone);
    }

    private void OnTriggerExit(Collider other)
    {
        SnapZone snapZone = other.GetComponent<SnapZone>();
        if (snapZone)
        {
            snapZones.Remove(snapZone);
        }
    }

    public void Remove(SnapZone snapZone)
    {
        snapZones.Remove(snapZone);
    }

    private void FixedUpdate()
    {
        SnapZone closest = TryGetClosestInteractable();

        // nothing has changed
        if (closest == CurrentSnapZone) { return; }

        // something has changed (maybe null)
        CurrentSnapZone = closest;
    }

    private SnapZone TryGetClosestInteractable()
    {
        var minDistance = float.MaxValue;
        SnapZone closest = null;
        foreach (var snapZone in snapZones)
        {
            var distance = Vector3.Distance(playerPivot.position, snapZone.gameObject.transform.position);
            if (distance > minDistance) continue;
            minDistance = distance;
            closest = snapZone;
        }

        return closest;
    }
}
