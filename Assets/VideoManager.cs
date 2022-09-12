using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VideoManager : MonoBehaviour
{

    private void Start()
    {
        if (PlayerPrefs.HasKey("playerName"))
        {
            SceneManager.LoadScene(1);
        }
    }

}
