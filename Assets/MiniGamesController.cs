using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamesController : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Piece"))
        {
            other.GetComponent<Piece>().CheckIn();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Piece"))
        {
            other.GetComponent<Piece>().CheckOut();
        }
    }
}
