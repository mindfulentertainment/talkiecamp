using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlants : MonoBehaviour
{
    public  static PlantSeed seed;
    public  void  Create(int num )
    {
        seed.Plant(num);
    }
}
