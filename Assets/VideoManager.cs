using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer video;
    private void Start()
    {
        if (PlayerPrefs.HasKey("playerName"))
        {
            SkipVideo();

        }
        else
        {
            StartCoroutine(ContinueGame());

        }
    }

   IEnumerator ContinueGame()
    {

        yield return new WaitForSeconds(video.frameCount/video.frameRate);

        SceneManager.LoadScene(1);
    }

    public void SkipVideo()
    {
        SceneManager.LoadScene(1);
    }

}
