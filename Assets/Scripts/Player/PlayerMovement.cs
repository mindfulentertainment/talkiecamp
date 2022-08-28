using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] CharacterController characterController;
    [SerializeField] Joystick joystick;
    public float speed;
    public float smoothTurnTime;
    private float smoothTurnVelocity;

    [Header("PickUpObjects")]
    [SerializeField] private Transform slot;
    private PickAndDrop pickAndDrop;
    private IPickable _currentPickable;

    private void Awake()
    {
        pickAndDrop = GetComponentInChildren<PickAndDrop>();
    }

    private void Update()
    {
        Movement();

        if (Input.GetButtonDown("Jump"))
        {
            HandlePickUp();
        }
    }

    private void Movement()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            characterController.Move(direction * speed * Time.deltaTime);
        }
    }

    private void HandlePickUp()
    {
        var snapZone = pickAndDrop.CurrentSnapZone;

        // empty hands, try to pick
        if (_currentPickable == null)
        {
            _currentPickable = snapZone as IPickable;
            if (_currentPickable != null)
            {
                _currentPickable.Pick();
                pickAndDrop.Remove(_currentPickable as SnapZone);
                _currentPickable.gameObject.transform.SetPositionAndRotation(slot.transform.position,Quaternion.identity);
                _currentPickable.gameObject.transform.SetParent(slot);
                return;
            }

            // SnapZone only (not a IPickable)
            _currentPickable = snapZone?.TryToPickUpFromSlot(_currentPickable);

            _currentPickable?.gameObject.transform.SetPositionAndRotation(slot.position, Quaternion.identity);
            _currentPickable?.gameObject.transform.SetParent(slot);
            return;
        }

        // we carry a pickable, let's try to drop it

        // no snap zone in range or at most a Pickable in range (we ignore it)
        if (snapZone == null || snapZone is IPickable)
        {
            _currentPickable.Drop();
            _currentPickable = null;
            return;
        }

        // we carry a pickable and we have an snap zone in range
        // we may drop into the snap zone

        // Try to drop on the snap zone. It may refuse it, e.g. dropping a plate into the CuttingBoard,
        // or simply it already have something on it
        //Debug.Log($"[PlayerController] {_currentPickable.gameObject.name} trying to drop into {interactable.gameObject.name} ");

        bool dropSuccess = snapZone.TryToDropIntoSlot(_currentPickable);
        if (!dropSuccess) return;

        _currentPickable = null;
    }
}
