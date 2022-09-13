using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundIntance;

    [SerializeField] AudioSource audioSource;


    private void Awake()
    {

       // AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        if (soundIntance != null && soundIntance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        soundIntance = this;
        DontDestroyOnLoad(this);

    }

    public void VolumeSound(float num)
    {
        audioSource.volume = num;
    }
}
