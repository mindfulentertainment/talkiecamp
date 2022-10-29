using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
