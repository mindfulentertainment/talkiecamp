using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundIntance;


    private void Awake()
    {
        if (soundIntance != null && soundIntance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        soundIntance = this;
        DontDestroyOnLoad(this);

    }
}
