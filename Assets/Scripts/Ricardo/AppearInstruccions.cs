using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearInstruccions : MonoBehaviour
{
    
    [SerializeField] GameObject instructions;
    [SerializeField] float timeForInst;
    [SerializeField] REvents startInstructions;
    void Start()
    {
        instructions.SetActive(false);
        startInstructions.GEvent += DisplayInst;
    }
    void DisplayInst()
    {
        StartCoroutine("DisplayInstructions");
    }
    IEnumerator DisplayInstructions()
    {
        instructions.SetActive(true);
        yield return new WaitForSeconds(timeForInst);
        instructions.SetActive(false);
    }
    private void OnDestroy()
    {
        startInstructions.GEvent -= DisplayInst;
    }

}
