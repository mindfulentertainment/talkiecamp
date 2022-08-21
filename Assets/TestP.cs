using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestP : MonoBehaviour
{
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rb.velocity =new Vector3(1,2,3);
        }
    }
}
