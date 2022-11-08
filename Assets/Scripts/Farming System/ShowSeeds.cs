using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ShowSeeds : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lechugaText, cebollaText, tomateText, trigoText;
    private int lechuga, cebolla, tomate, trigo;
    private void Update()
    {

        cebolla = PlayerPrefs.GetInt("onion");
        tomate = PlayerPrefs.GetInt("tomato");
        lechuga = PlayerPrefs.GetInt("lettuce");
        trigo = PlayerPrefs.GetInt("wheat");

        lechugaText.text = lechuga.ToString();
        cebollaText.text = cebolla.ToString();
        tomateText.text = tomate.ToString();
        trigoText.text = trigo.ToString();
    }
}

