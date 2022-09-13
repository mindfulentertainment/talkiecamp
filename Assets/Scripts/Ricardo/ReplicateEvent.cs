using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplicateEvent : MonoBehaviour
{
    [SerializeField] REvents rEvents;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rEvents.FireEvent();
        }
    }
    //private void OnEnable()
    //{
    //    UIController.instance.pickBtn.onClick.AddListener(() => rEvents.FireEvent());
    //}
    //private void OnDisable()
    //{
    //    UIController.instance.pickBtn.onClick.RemoveListener(() => rEvents.FireEvent());

    //}
}
