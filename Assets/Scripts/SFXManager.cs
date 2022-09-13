using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource AudioSource;
   

    public void PlaySound(int num)
    {
       AudioSource.clip = clips[num];
        AudioSource.Play(); 
    }

    public void VolumeSound(float num)
    {
        AudioSource.volume = num;
    }
}
