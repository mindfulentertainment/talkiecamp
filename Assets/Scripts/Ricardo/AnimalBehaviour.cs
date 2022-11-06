using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{
    [SerializeField] float speed,waitTime,startWaitTime,angle; //startWaitTime es el que se le debe cambiar la variable en el inspector
    [SerializeField] Transform[] moveSpots;
    [SerializeField] int randomSpot;
    [SerializeField] bool trapped;
    [SerializeField] Vector3 dif;
    

    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        RotateTo();
    }
    
    void Update()
    {
        if (trapped == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                    RotateTo();
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
        
    }
    void RotateTo()
    {
        dif = (moveSpots[randomSpot].position - transform.position).normalized;
        angle = Mathf.Atan2(dif.z, dif.x);
        transform.rotation = Quaternion.Euler(90f, 0f, angle * Mathf.Rad2Deg);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            trapped = true;
            transform.position = other.transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            trapped = false;
        }
    }
}
