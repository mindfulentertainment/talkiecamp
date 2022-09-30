using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryGame : MonoBehaviour
{
    [SerializeField] REvents game;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            game.FireEvent();
        }
    }
}
