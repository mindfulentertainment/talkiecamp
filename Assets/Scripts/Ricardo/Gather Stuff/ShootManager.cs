using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    [SerializeField] float tbs;
    [SerializeField] REvents start;
    [SerializeField] REvents[] shouts;
    void Start()
    {
        start.GEvent += ShootAll;
    }

    void ShootAll()
    {
        StartCoroutine("ShootStuff");
    }
    IEnumerator ShootStuff()
    {
        for (int i = 0; i < shouts.Length; i++)
        {
            shouts[i].FireEvent();
            yield return new WaitForSeconds(tbs);
        }
    }
    private void OnDestroy()
    {
        start.GEvent -= ShootAll;
    }
}
