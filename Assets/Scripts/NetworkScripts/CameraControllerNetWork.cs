using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraControllerNetWork : MonoBehaviourPun
{
    private Transform target;
    public  Transform player;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset;
    public static CameraControllerNetWork instance;
    private Vector3 velocity = Vector3.zero;
    private Camera m_camera;
    Coroutine m_Coroutine;
    private void Awake()
    {
        Application.targetFrameRate = 300;
        m_camera = Camera.main;
        instance = this;
    }
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

    public void ChangeTarget(Transform pos)
    {
        if (m_Coroutine != null)
        {
            StopCoroutine(m_Coroutine);
        }
        target = pos;
        smoothSpeed = 0.8f;
        m_camera.fieldOfView = 22;
    }

    public void CenterPlayer()
    {
        target = player;
        if (m_Coroutine != null)
        {
            StopCoroutine(m_Coroutine);
        }
        m_Coroutine = StartCoroutine(CameraTransition());

        

    }
    public void  Check(bool zoom)
    {
        if (zoom&& m_Coroutine==null)
        {
            smoothSpeed = 0.8f;
            m_camera.fieldOfView = 22;
            m_camera.fieldOfView = Mathf.Lerp(m_camera.fieldOfView, 7, Time.deltaTime);
        }
        else if(m_Coroutine == null)
        {
            CenterPlayer();
        }
    }
   
    IEnumerator CameraTransition()
    {
        bool isFinished=true;
        while (isFinished)
        {

            m_camera.fieldOfView = Mathf.Lerp(m_camera.fieldOfView, 7, Time.deltaTime);
            smoothSpeed = Mathf.Lerp(smoothSpeed, 0.25f, 0.01f);
            if (m_camera.fieldOfView <= 7.1f)
            {
                isFinished =false;
            }
            yield return null;
        }
        m_Coroutine = null;
    }

    IEnumerator GetPlayer()
    {
        yield return new WaitForSeconds(0.6f);

        player = PlayerSpawner.instance.player.transform;
        target = player;
    }
}
