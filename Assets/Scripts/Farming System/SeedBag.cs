using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBag : MonoBehaviour
{
    public   int onion, tomato, lettuce, wheat;
    float t; 
   

    private void Start()
    {
        onion = PlayerPrefs.GetInt("onion");
        tomato = PlayerPrefs.GetInt("tomato");
        lettuce = PlayerPrefs.GetInt("lettuce");
        wheat = PlayerPrefs.GetInt("wheat");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onion += RandomSeed();
            tomato += RandomSeed();
            lettuce += RandomSeed();
            wheat += RandomSeed();

            PlayerPrefs.SetInt("onion",onion);
            PlayerPrefs.SetInt("tomato", tomato);
            PlayerPrefs.SetInt("lettuce", lettuce);
            PlayerPrefs.SetInt("wheat", wheat);

            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            t = 0;
        }
    }

    private void Update()
    {
        t += Time.deltaTime;
        if (t >= 30f)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<SphereCollider>().enabled = true;
            t = 0;
        }

        transform.Rotate(0, 0, 50f*Time.deltaTime);
    }


    private int RandomSeed()
    {
        int num = Random.Range(3, 9);
        return num;
    }

}
