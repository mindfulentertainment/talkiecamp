using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EmotionDisplay : MonoBehaviour
{
    [SerializeField] Sprite[] emojies;
    [SerializeField] Image emotionImage;
    bool isOn;
    Coroutine coroutine;
    private Transform camera;
    void Awake()
    {

        camera = Camera.main.transform;

    }

    
    private void Update()
    {
        if (isOn)
        {
            transform.LookAt(camera);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime*1.7f);
        }
    }
    public void GiveEmotion(int index)
    {
        emotionImage.sprite= emojies[index];
        emotionImage.gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        isOn = true;
        if(coroutine != null)
        {
            StopCoroutine(coroutine);

        }
        coroutine =StartCoroutine(ImageOff());
        
    }

   
    

    IEnumerator ImageOff()
    {
        yield return new WaitForSeconds(4);
        emotionImage.gameObject.SetActive(false);
        transform.localScale= Vector3.zero; 
        isOn = false;
    }
} 
