using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Floor Pattern")]
public class FloorPattern : ScriptableObject
{

    public bool[] targetPiece= new bool[9];
    public Sprite pattern;
}
