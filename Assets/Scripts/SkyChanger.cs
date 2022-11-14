using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyChanger : MonoBehaviour
{
    public Material skyBox1;
    public Material skyBox2;
    public bool encender;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(encender == true)
        {
            RenderSettings.skybox = skyBox1;
        }
        else
        {
            RenderSettings.skybox = skyBox2;
        }
    }
}
