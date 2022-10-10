using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleSprite : MonoBehaviour
{
 
    public Sprite spriteBuilder;
    public Sprite spriteWatchmen;
    public Sprite spriteGatherer;
    public Sprite spriteChef;
    public Image spriteImage;
    private void Start()
    {
        spriteImage = GetComponent<Image>();
    }
    public void SwitchSprite(Sprite sprite)
    {
        spriteImage.sprite = sprite;
    }
}
