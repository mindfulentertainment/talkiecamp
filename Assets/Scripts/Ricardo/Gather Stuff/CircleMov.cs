using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMov : MonoBehaviour
{
    [SerializeField] Transform recipe;
    [SerializeField] Vector3 pos;
    [SerializeField] float height,rad,tetha,angularSpeed;
    void Start()
    {
        
        transform.position = recipe.position;
    }

    // Update is called once per frame
    void Update()
    {
        tetha += angularSpeed * Time.deltaTime;
        pos = PolarToCartesian(rad, tetha, height);
        transform.position = pos+ recipe.position;
    }
    Vector3 PolarToCartesian(float radio, float tetha,float altitude)
    {
        return new Vector3(radio * Mathf.Cos(tetha),altitude, radio * Mathf.Sin(tetha));
    }
}
