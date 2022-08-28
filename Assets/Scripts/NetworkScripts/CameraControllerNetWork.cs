using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraControllerNetWork : MonoBehaviourPun
{
    private Transform target;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        StartCoroutine(GetPlayer());
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothPosition;
        }
    }

    IEnumerator GetPlayer()
    {
        yield return new WaitForSeconds(0.6f);

        target = PlayerSpawner.instance.player.transform;
    }
}
