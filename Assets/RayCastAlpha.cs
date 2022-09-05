using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class RayCastAlpha : MonoBehaviour
{



     Image theButton;
    private void Awake()
    {
        theButton=GetComponent<Image>();
    }
    // Use this for initialization
    void Start()
    {
        theButton.alphaHitTestMinimumThreshold = 0.5f;
    }

}
