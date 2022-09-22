using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointToDestination : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 destiny;
    [SerializeField] Vector3 dir;
    [SerializeField] float distanceBtwPlayer, angle;


    bool hasDestiny=false;
    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        DataManager.instance.OnNewBuilding += CheckMegaphone;
    }
    private void OnDisable()
    {
        DataManager.instance.OnNewBuilding -= CheckMegaphone;
    }
    void CheckMegaphone(Resource resource, Buildings buildings)
    {
        foreach (var item in buildings.buildings)
        {
            if (item.buildingName == "Megaphone")
            {
                destiny = item.GetPosition();
                hasDestiny = true;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(hasDestiny)
        {
            dir = (destiny - transform.position).normalized;
            transform.position = player.transform.position + dir * distanceBtwPlayer;
            angle = Mathf.Atan2(dir.z, dir.x);
            transform.rotation = Quaternion.Euler(90f, 0f, angle * Mathf.Rad2Deg);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CallCenter"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
