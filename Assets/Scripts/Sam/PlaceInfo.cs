using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "PlaceInfo", menuName = "ScriptableObjects/New place info", order = 1)]
public class PlaceInfo : ScriptableObject
{
    public string placeName;
    public int wood;
    public int stone;
    public int fabric;
    public int meat;
    public int hamburguers;
    public int sandwiches;
    public int soups;
    public int conexion;
}
