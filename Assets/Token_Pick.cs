using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token_Pick : MonoBehaviour
{
    public int key;

    public bool isAvailable=true;
    void Start()
    {
        CreateToken();
        Debug.Log(this.gameObject.GetHashCode());

    }
    

    public virtual void CreateToken()
    {
        IPickable pickable = GetComponent<IPickable>();
        Token_Manager.DefaultInstance.pickables_tokens.Add(key, pickable);
    }


}
