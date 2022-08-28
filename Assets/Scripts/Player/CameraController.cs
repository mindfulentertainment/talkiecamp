using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset;

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothPosition;
    }
}
