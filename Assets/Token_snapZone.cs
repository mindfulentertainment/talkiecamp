using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token_snapZone : MonoBehaviour
{
    public int key;
    void Start()
    {
        SnapZone pickable = GetComponent<SnapZone>();
        Token_Manager.DefaultInstance.snapzones_tokens.Add(key, pickable);
    }

}
