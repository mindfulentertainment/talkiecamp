using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : SnapZone
{

    public Sprite lettuce;
    public Sprite ham;
    public Sprite tomato;
    public Sprite carrot;
    public Sprite bun;
    public Sprite stone;
    public Sprite fabric;
    public Sprite wood;
    public Sprite meat;
    public Image actualSrite;
    public GameObject placeholder;
    Vector3 normal ;
    private void Start()
    {
        normal = placeholder.transform.localPosition;
        placeholder.gameObject.SetActive(false);
    }
    public override bool TryToDropIntoSlot(IPickable pickable)
    {
        if (pickable == null|| pickable.gameObject.GetComponent<Element>()==null)
        {
            return false;
        }
        else
        {
            CurrentPickable = pickable;
            Element element;
            if (CurrentPickable.gameObject != null)
            {
                CurrentPickable.gameObject.TryGetComponent(out element);
               
                DataManager.instance.IncreaseElement(element);

                CurrentPickable.gameObject.transform.SetParent(Slot);
                CurrentPickable.gameObject.transform.SetPositionAndRotation(Slot.position, Quaternion.identity);
                CurrentPickable.gameObject.SetActive(false);
                GameObject toDestroy = CurrentPickable.gameObject;
                CurrentPickable = null;
                ShowElement(element);
                Destroy(toDestroy.gameObject, 2f);
            }
            return true;


        }



    }

    public override IPickable TryToPickUpFromSlot(IPickable playerHoldPickable)
    {
        if (CurrentPickable == null) return null;

        var output = CurrentPickable;
        CurrentPickable = null;
        return output;
    }

    private bool TryDropIfNotOccupied(IPickable pickable)
    {

       

        return false;
    }


    public void ShowElement(Element element)
    {

        switch (element.type)
        {
            case Element.ElementType.wood:
                actualSrite.sprite = wood;
                break;
            case Element.ElementType.ham:
                actualSrite.sprite = ham;
                break;
            case Element.ElementType.tomato:
                actualSrite.sprite = tomato;
                break;
            case Element.ElementType.carrot:
                actualSrite.sprite = carrot;
                break;
            case Element.ElementType.bun:
                actualSrite.sprite = bun;
                break;
            case Element.ElementType.stone:
                actualSrite.sprite = stone;
                break;
            case Element.ElementType.fabric:
                actualSrite.sprite = fabric;
                break;
            case Element.ElementType.meat:
                actualSrite.sprite = meat;
                break;
            case Element.ElementType.lettuce:
                actualSrite.sprite = lettuce;
                break;
            default:
                break;
        }
        AnimSrpite();
   
}

    public void AnimSrpite()
    {

        placeholder.transform.localPosition = normal;
        placeholder.gameObject.SetActive(true);
        Vector3 toUp = new Vector3(normal.x, normal.y +3, normal.z);

        LeanTween.moveLocalY(placeholder, to: 5, time: 1).setOnComplete(() =>
        {

            placeholder.gameObject.SetActive(false);

        });
        Color toColor = new Color(0, 0, 0, 0);
        Color fromColor = new Color(255, 255, 255, 1);
        LeanTween.value(actualSrite.gameObject, (c) => actualSrite.color = c, fromColor, toColor, 1);
       
    }
}
