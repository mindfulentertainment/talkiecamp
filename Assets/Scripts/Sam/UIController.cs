using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Joystick joystick;
    private string role;
    public TMP_Text roleText;
    private void Awake()
    {
        instance = this;
    }
   void Start()
    {
        role = PlayerPrefs.GetString("role");
        roleText.text = role;
    }
}
