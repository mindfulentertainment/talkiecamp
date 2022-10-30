using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{

    private void Start()
    {
        float time = 0;
        if (Time.timeSinceLevelLoad > 5f) time = 4;

        StartCoroutine(Destroy(time));
    }

    IEnumerator Destroy(float time)
    {

        yield return new WaitForSeconds(time);
        DestroyImmediate(this.gameObject);
    }
}
