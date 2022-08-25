using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CampButton : MonoBehaviour
{
    public TMP_Text campName;

    public void SetButtonDetails(string campName)
    {
        this.campName.text = campName;
    }
    public void OpenRoom()
    {

    }
}
