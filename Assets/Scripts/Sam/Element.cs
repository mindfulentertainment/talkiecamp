using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public  class Element: MonoBehaviour
{
    public int amount;

    public Element(ElementType element, int amount)
    {
        this.amount = amount;
        this.type=element;
    }


    public enum ElementType
    {
        wood,
        stone,
        fabric,
        meat,
        connection,
        hamburguer,
        sandwich,
        soup

    }
    public ElementType type;
    public  int GetAmount()
    {
        return amount;

    }

}