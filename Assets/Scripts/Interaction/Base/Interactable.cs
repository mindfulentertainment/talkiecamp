using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Interactable : MonoBehaviourPunCallbacks
{
    [PunRPC]
    public virtual void OnInteraction()
    {
        Debug.Log("Interaction");
    }

    public virtual void CreatePlant(GameObject plant)
    {
        Debug.Log("plant");
    }

    public float OnDestruction(Slider slider, float timeOfDestruction, float timer)
    {
        slider.maxValue = timeOfDestruction;
        slider.value = 0f;
        if (Input.GetMouseButton(0))
        {
            slider.gameObject.SetActive(true);
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        
        slider.value = timer;

        return timer;
    }

}
