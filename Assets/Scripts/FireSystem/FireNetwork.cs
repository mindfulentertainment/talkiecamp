using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireNetwork : MonoBehaviourPun
{
    [SerializeField , Range (0f,1f)] private float currentIntensity;
    [SerializeField] private ParticleSystem firePs;
    
    [SerializeField] private float regenerationDelay;
    [SerializeField] private float regenerationRate;
     private Slider slider;
    private Transform playerPos;
   // [SerializeField] private Animator animator;
   
    private float lastTimeWatered = 0f;
    private float startIntensity = 10;
    private bool isActive=true;

    ActivationManager activationManager;
   
    void Start()
    {
        activationManager = GetComponent<ActivationManager>();
        slider = UIController.instance.slider;
        playerPos = PlayerSpawner.instance?.player.transform;
        //  startIntensity = firePs.emission.rateOverTime.constant;
        currentIntensity = 1.0f;
    }

    private void Update()
    {

        
        if (slider != null && playerPos!=null)
        {
            RegenerationRate();
        }
        else
        {
            playerPos = PlayerSpawner.instance?.player.transform;
        }

    }
    
   
    
    public void RegenerationRate()
    {
        if (Vector3.Distance(playerPos.position, transform.position) <= 10f)
        {

           

            if (isActive && currentIntensity < 1.0f && Time.time - lastTimeWatered >= regenerationDelay)
            {
                currentIntensity += regenerationRate * Time.deltaTime;
                slider.value = currentIntensity;
                ChangeIntensity();

            }
        }
        else
        {
            slider.gameObject.SetActive(false);
        }
    }
 
    public void TryExtinguish( float amount)
    {
        if(activationManager.onFire == true)
        {
            slider.gameObject.SetActive(true);
        }
        
        lastTimeWatered = Time.time; //actual time 
       
        currentIntensity -= amount;
        
        slider.value = currentIntensity;
      
        ChangeIntensity();

        if (currentIntensity <= 0)
        {
           // animator.SetBool("isDone",true);
            
            photonView.RPC("Gone", RpcTarget.AllViaServer);
           
           
            
        }
    }
    private void ChangeIntensity()
    {
        var emission = firePs.emission;
        emission.rateOverTime = currentIntensity * startIntensity;
    }

    [PunRPC]
    private void Gone()
    {
        var emissonRate = firePs.emission;
        emissonRate.rateOverTime = startIntensity;
        slider.gameObject.SetActive(false);
        //animator.SetBool("isDone", false);
        firePs.gameObject.SetActive(false);
        activationManager.onFire = false;
        currentIntensity = 1;
    }
}
