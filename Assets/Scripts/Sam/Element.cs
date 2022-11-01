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
        hamburguer_2,
        sandwich,
        sandwich_2,
        soup,
        soup_2,
        tomato,
        lettuce,
        carrot,
        bun,
        ham

    }
    public ElementType type;
    public  int GetAmount()
    {
        return amount;

    }

}