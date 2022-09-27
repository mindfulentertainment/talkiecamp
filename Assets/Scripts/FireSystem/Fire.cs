using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    [SerializeField , Range (0f,1f)] private float currentIntensity;
    [SerializeField] private ParticleSystem firePs;
    
    [SerializeField] private float regenerationDelay;
    [SerializeField] private float regenerationRate;
    [SerializeField] private Slider slider;
    [SerializeField] private Animator animator;
   
    private float lastTimeWatered = 0f;
    private float startIntensity = 10;
    private bool isActive = true;


   
    void Start()
    {

      
        //  startIntensity = firePs.emission.rateOverTime.constant;
        currentIntensity = 1.0f;
    }

    private void Update()
    {
        //slider.gameObject.SetActive(false);
        RegenerationRate();
    }
   
    
    public void RegenerationRate()
    { 
        if (isActive && currentIntensity < 1.0f && Time.time - lastTimeWatered >= regenerationDelay)
        {
            currentIntensity += regenerationRate * Time.deltaTime;
            slider.value = currentIntensity;
            ChangeIntensity();
        }
    }
 
    public void TryExtinguish( float amount)
    {
        slider.gameObject.SetActive(true);
        lastTimeWatered = Time.time; //actual time 
       
        currentIntensity -= amount;
        slider.value = currentIntensity;
      
        ChangeIntensity();

        if (currentIntensity <= 0)
        {
            animator.SetBool("isDone",true);
            var emissonRate = firePs.emission;
            emissonRate.rateOverTime = startIntensity;
            Invoke("Gone", 1f);
            currentIntensity = 1;
            isActive = true;
            
        }
    }
    private void ChangeIntensity()
    {
        var emission = firePs.emission;
        emission.rateOverTime = currentIntensity * startIntensity;
    }

    private void Gone()
    {
        slider.gameObject.SetActive(false);
        animator.SetBool("isDone", false);
        this.gameObject.SetActive(false);
    }
}
