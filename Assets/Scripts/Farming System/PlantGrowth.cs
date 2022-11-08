using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
  
    public int currentStage = 0; 
    [SerializeField] private int timeTweenGrowth;
    [SerializeField] private int maxGrowth;
   
    void Start()
    {
        
        StartCoroutine(Growth());
    }

  

    IEnumerator Growth()
    {
        while (true)
        {
           
            yield return new WaitForSeconds(timeTweenGrowth);

            if (currentStage != maxGrowth)
            {
                gameObject.transform.GetChild(currentStage).gameObject.SetActive(true);
                timeTweenGrowth += 45;
               
            }
            if (currentStage > 0 && currentStage < maxGrowth)
            {
                gameObject.transform.GetChild(currentStage - 1).gameObject.SetActive(false);

            }
            if (currentStage < maxGrowth)
            {
                currentStage++;
               
            }
            if(currentStage == maxGrowth)
            {
                gameObject.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = true; 
            }
        }
    }

   
}
