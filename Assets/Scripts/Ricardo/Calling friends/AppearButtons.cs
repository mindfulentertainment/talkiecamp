using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearButtons : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    void Start()
    {
        canvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(false);
        }
    }
}
