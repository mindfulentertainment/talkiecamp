using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialAnimation : MonoBehaviour
{

    public Image imgTittle;
    public Image barUp;
    public Image barDown;
    public GameObject advicePanel;
    public GameObject cameraTutorial;
    public GameObject canvas;
    public GameObject tutorialObject;
    public GameObject tutorialCanvas;
    void Start()
    {
        StartCoroutine(StartTweenImage());
        StartCoroutine(StartTweenBars());
        cameraTutorial.SetActive(true);
        canvas.SetActive(false);
    }


    IEnumerator StartTweenImage()
    {
        yield return new WaitForSeconds(2);

        Color fromColor = new Color(0, 0, 0, 0);
        Color toColor = new Color(255, 255, 255, 1);
        var seq = LeanTween.sequence();

        LeanTween.value(imgTittle.gameObject, (c) => imgTittle.color = c, fromColor, toColor, 1);

        seq.append(LeanTween.scale(imgTittle.gameObject, to: this.gameObject.transform.localScale * 1.2f, time: 0.6f));
        yield return new WaitForSeconds(3);

        fromColor = new Color(255, 255, 255, 1); 
        toColor = new Color(0, 0, 0, 0);

        LeanTween.value(imgTittle.gameObject, (c) => imgTittle.color = c, fromColor, toColor, 1);
        yield return new WaitForSeconds(1);
        advicePanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(4.3f);
        advicePanel.gameObject.SetActive(false);


    }

    IEnumerator StartTweenBars()
    {
        yield return new WaitForSeconds(1);

        float fromHeight = 0;
        float toHeight = 50;

        LeanTween.value(barDown.gameObject, (w) => barDown.rectTransform.sizeDelta = new Vector2(1920, w), fromHeight, toHeight, 1);
        LeanTween.value(barUp.gameObject, (w) => barUp.rectTransform.sizeDelta = new Vector2(1920,w), fromHeight, toHeight, 1);
        yield return new WaitForSeconds(10);

        fromHeight = 50;
        toHeight = 700;

        LeanTween.value(barDown.gameObject, (w) => barDown.rectTransform.sizeDelta = new Vector2(1920, w), fromHeight, toHeight, 1);
        LeanTween.value(barUp.gameObject, (w) => barUp.rectTransform.sizeDelta = new Vector2(1920, w), fromHeight, toHeight, 1);
        
        yield return new WaitForSeconds(1f);
        canvas.gameObject.SetActive(true);
        tutorialCanvas.SetActive(true);
        cameraTutorial.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        fromHeight = 700;
        toHeight = 0;
        LeanTween.value(barDown.gameObject, (w) => barDown.rectTransform.sizeDelta = new Vector2(1920, w), fromHeight, toHeight, 1);
        LeanTween.value(barUp.gameObject, (w) => barUp.rectTransform.sizeDelta = new Vector2(1920, w), fromHeight, toHeight, 1);
        yield return new WaitForSeconds(1f);
        tutorialObject.SetActive(false);

    }


}
