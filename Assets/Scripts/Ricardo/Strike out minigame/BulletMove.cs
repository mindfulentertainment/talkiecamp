using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] Vector3 dir;
    [SerializeField] float force,duration;
    
    public Vector3 canon;
    public bool reverse;
    

    
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
        StartCoroutine("BulletLife");
    }
    private void Update()
    {
        transform.Translate(dir*force * Time.deltaTime, Space.World);
    }

    IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(duration);
        //se detiene
        transform.Translate(dir, Space.World);
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            var tp = other.GetComponentInParent<PlayerTP>();
            if (tp != null)
            {
                tp.TPOut();
            }
            StopCoroutine("BulletLife");
             //se detiene
            transform.Translate(dir, Space.World);
            this.gameObject.SetActive(false);
            
        }
    }
}
