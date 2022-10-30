using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHints : MonoBehaviour
{
    public GameObject lockImage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowTutorialHints());
    }

   
    IEnumerator ShowTutorialHints()
    {
        UIController.instance.lockImage.SetActive(true);
        UIController.instance.joystick.gameObject.SetActive(false);
        UIController.instance.pickBtn.gameObject.SetActive(false);
        UIController.instance.roleText.gameObject.SetActive(false);
        UIController.instance.resourcesBtn.gameObject.SetActive(false);
        UIController.instance.emoticonBtn.gameObject.SetActive(false);
        UIController.instance.zoomBtn.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.8f);
        transform.GetChild(0).gameObject.SetActive(true);
        UIController.instance.joystick.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        UIController.instance.pickBtn.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        UIController.instance.roleText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
        UIController.instance.resourcesBtn.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(true);
        UIController.instance.emoticonBtn.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        transform.GetChild(4).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(true);
        UIController.instance.zoomBtn.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        UIController.instance.lockImage.SetActive(false);
        lockImage.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(6).gameObject.SetActive(true);
        DataManager.instance.resource.newCamp = true;

    }
}
