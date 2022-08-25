using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBallBigger : Interactable
{
   

    public override void OnInteraction()
    {
        //Material material = GetComponent<Material>();
        //material.SetColor("_Color", Color.blue);
        transform.localScale = new Vector3(3,3,3);
        Debug.Log("override");
    }
}
