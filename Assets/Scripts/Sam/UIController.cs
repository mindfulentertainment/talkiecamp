using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider weaponTempSlider;
    public Slider healthSlider;
    public GameObject deathScreen;
    public TMP_Text deathText;
    public TMP_Text kills;
    public TMP_Text deaths;
    private void Awake()
    {
        instance = this;
    }
    public TMP_Text overheatedMessage;
}
