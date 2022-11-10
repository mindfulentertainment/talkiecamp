using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeFertileLand : Interactable
{

    float timer;
    [SerializeField] float timeOfDestruction;
    [SerializeField] Slider slider;
    [SerializeField] PlantSeed PlantSeed;
    int isFertilezed;

    private void Start()
    {
        slider = UIController.instance.slider;
        PlantSeed.IsInteractable=true;

        GameObject child = gameObject.transform.GetChild(0).gameObject;
        child.SetActive(true);
        child.transform.parent = gameObject.transform.parent;
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        if (PlantSeed.IsInteractable) {
            CreatePlants.seed = PlantSeed;

        }
    }
    public override void OnInteraction()
    {
        timer = OnDestruction(slider, timeOfDestruction, timer);
        if (timer >= timeOfDestruction)
        {
            GameObject child =  gameObject.transform.GetChild(0).gameObject;
            child.SetActive(true);
            child.transform.parent = gameObject.transform.parent;
            slider.gameObject.SetActive(false);
            isFertilezed = 1;
            PlayerPrefs.SetInt("fertiled", isFertilezed);
            Destroy(gameObject);
          
           
        }
        

    }

 
}
