using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public  class Element: MonoBehaviour
{
    public int amount;

    public enum ElementType
    {
        wood,
        stone

    }
    public ElementType type;
    public  int GetAmount()
    {
        return amount;

    }

}