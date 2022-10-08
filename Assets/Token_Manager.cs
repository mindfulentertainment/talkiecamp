using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token_Manager : MonoBehaviour
{
    public Dictionary<int, IPickable> pickables_tokens = new Dictionary<int, IPickable>();
    public Dictionary<int, SnapZone> snapzones_tokens = new Dictionary<int, SnapZone>();

    public static Token_Manager DefaultInstance;
    private void Awake()
    {
        DefaultInstance = this;
    }
}
