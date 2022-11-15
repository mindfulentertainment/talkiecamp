using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class ShootObject : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Transform movingDir,posIni;
    [SerializeField] float force;
    [SerializeField] REvents thing,dissapear;
   
    Vector3 initialSize;
    void Start()
    {
        transform.position = posIni.position;
        initialSize = transform.localScale;
        rb = GetComponent<Rigidbody>();
        thing.GEvent += Shoot;
        dissapear.GEvent += Disappear;
        transform.localScale = Vector3.zero;
        rb.isKinematic = true;
    }

    void Shoot()
    {
        transform.localScale = initialSize;
        rb.isKinematic = false;
        if (!PhotonNetwork.IsMasterClient)
        {
            GetComponent<SmoothSyncMovement>().enabled = true;

        }
        else
        {
            rb.AddForce((movingDir.transform.position - transform.position) * force * Time.deltaTime);
            GetComponent<SmoothSyncMovement>().enabled = false;
        }
        StartCoroutine(DisableSync());

    }
    public void Disappear()
    {
        rb.isKinematic=true;
        
        transform.position = posIni.position;
        transform.localScale = Vector3.zero;
    }
    private void OnDestroy()
    {
        thing.GEvent -= Shoot;
        dissapear.GEvent -= Disappear;
    }
    IEnumerator DisableSync() {

        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => rb.velocity == Vector3.zero);
        Debug.Log("ssssssssssssssssssssssss");
        GetComponent<SmoothSyncMovement>().enabled = false;

    }
}
