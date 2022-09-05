using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class MakeBallBigger : Interactable
{
    float timer;
    [SerializeField] float timeOfDestruction;
    [SerializeField] Slider slider;
    [PunRPC]
    public override void OnInteraction()
    {
        timer = OnDestruction(slider, timeOfDestruction, timer);
        if (timer >= timeOfDestruction)
        {
            slider.gameObject.SetActive(false);
            transform.localScale = new Vector3(3, 3, 3);
            Debug.Log("override");
        }
    }
}
