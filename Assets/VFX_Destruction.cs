using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Destruction : MonoBehaviour
{
    public static VFX_Destruction instance;
    public ParticleSystem ps;
    Coroutine vfxCoroutine;
    [SerializeField] AudioSource audioSource;
    private void Awake()
    {
        instance = this;
    }

    public void SetVFX(Vector3 pos)
    {
        if (vfxCoroutine != null)
        {
            StopCoroutine(vfxCoroutine);
            vfxCoroutine = null;
        }
        vfxCoroutine = StartCoroutine(TurnOffVFX(pos));
    }
    IEnumerator TurnOffVFX(Vector3 pos)
    {
        gameObject.transform.position = new Vector3(pos.x, gameObject.transform.position.y, pos.z);
        ps.gameObject.SetActive(true);
        ps.Play();
        audioSource.Play();
        yield return new WaitForSeconds(5);
        ps.gameObject.SetActive(false);
        vfxCoroutine = null;

    }

}
