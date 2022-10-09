using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObject : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Transform movingDir,posIni;
    [SerializeField] float force;
    [SerializeField] REvents thing,dissapear;
    Vector3 tama�oInicial;
    void Start()
    {
        transform.position = posIni.localPosition;
        tama�oInicial = transform.localScale;
        rb = GetComponent<Rigidbody>();
        thing.GEvent += Shoot;
        dissapear.GEvent += Disappear;
        transform.localScale = Vector3.zero;
        rb.isKinematic = true;
    }

    void Shoot()
    {
        transform.localScale = tama�oInicial;
        rb.isKinematic = false;
        rb.AddForce((movingDir.transform.position - transform.position) * force*Time.deltaTime);
    }
    public void Disappear()
    {
        rb.isKinematic=true;
        
        transform.position = posIni.localPosition;
        transform.localScale = Vector3.zero;
    }
    private void OnDestroy()
    {
        thing.GEvent -= Shoot;
        dissapear.GEvent -= Disappear;
    }
}
