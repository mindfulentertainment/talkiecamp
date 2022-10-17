using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearInstruccions : MonoBehaviour
{
    
    [SerializeField] GameObject instructions;
    [SerializeField] float timeForInst,appearTime;
    [SerializeField] REvents startInstructions;
    [SerializeField] Vector3 initialSize;
    void Start()
    {
        initialSize = instructions.transform.localScale;
        instructions.transform.localScale = Vector3.zero;
        startInstructions.GEvent += DisplayInst;
    }
    void DisplayInst()
    {
        StartCoroutine("DisplayInstructions");
    }
    IEnumerator DisplayInstructions()
    {
        instructions.transform.LeanScale(initialSize, appearTime).setEaseOutQuart();
        yield return new WaitForSeconds(timeForInst);
        instructions.transform.LeanScale(Vector3.zero, appearTime).setEaseOutQuart();
    }
    private void OnDestroy()
    {
        startInstructions.GEvent -= DisplayInst;
    }

}
