using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSong : MonoBehaviour
{
    [SerializeField] AudioSource megaphone;
    [SerializeField] AudioClip[] song;
    [SerializeField] REvents music;
    [SerializeField] int actualSong;
    void Start()
    {
        music.GEvent += ChangeAndReproduce;
        actualSong = Random.Range(0,song.Length);
        megaphone.clip = song[actualSong];
    }

    void ChangeAndReproduce()
    {
        int i= Random.Range(0, song.Length);
        if (i != actualSong)
        {
            actualSong = i;
            megaphone.Stop();
            megaphone.clip = song[actualSong];
            megaphone.Play();
        } 
        
    }
    private void OnDestroy()
    {
        music.GEvent -= ChangeAndReproduce;
    }
}
