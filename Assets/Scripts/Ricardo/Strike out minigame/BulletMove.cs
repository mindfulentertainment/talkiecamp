using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] Vector3 dir;
    [SerializeField] float force,duration;
    [SerializeField] REvents playerOut;
    public Vector3 canon;
    public bool reverse;
    [SerializeField] Rigidbody rb;

    
    void OnEnable()
    {
        if (reverse == false)
        {
            dir = (transform.position - canon).normalized;
        }
        else
        {
            
            dir = (canon - transform.position).normalized;
        }
        
       
        rb.AddForce(dir * force, ForceMode.Impulse);
        StartCoroutine("BulletLife");
    }

    IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(duration);
        rb.isKinematic = true;
        rb.isKinematic = false;
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            //Debug.Log("Player Out");


            var tp = other.GetComponentInParent<PlayerTP>();
            if (tp != null)
            {
                tp.TPOut();
            }
            StopCoroutine("BulletLife");
            rb.isKinematic = true;
            rb.isKinematic = false;
            this.gameObject.SetActive(false);
            playerOut.FireEvent();
        }
    }
}
