using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointToDestination : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform destiny;
    [SerializeField] Vector3 dir;
    [SerializeField] float distanceBtwPlayer, angle;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        dir = (destiny.position-transform.position).normalized;
        transform.position = player.transform.position+dir*distanceBtwPlayer;
        angle = Mathf.Atan2(dir.z, dir.x);
        transform.rotation = Quaternion.Euler(90f, 0f, angle * Mathf.Rad2Deg);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CallCenter"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
