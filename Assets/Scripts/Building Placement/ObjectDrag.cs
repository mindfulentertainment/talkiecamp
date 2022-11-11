using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offSet;
    private Vector3 lastPoint;
    public LayerMask BuildingZone;
    // static  BuildingSystem buildingSystem;
    private void Start()
    {
        UIController.instance.pickBtn.gameObject.SetActive(false);

    }

    private void OnDisable()
    {
        UIController.instance.pickBtn.gameObject.SetActive(true);

    }
    public Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 1000f, BuildingZone))
        {
            lastPoint = raycastHit.point;
            return raycastHit.point;
        }
        return lastPoint;
    }
    private void OnMouseDown()
    {
        offSet = transform.position - GetMousePosition();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hex"))
        {
            GetComponentInChildren<Outline>().OutlineColor=Color.black;
            UIController.instance.pickBtn.gameObject.SetActive(true);

        }

    }
    private void OnMouseDrag()
    {
        Vector3 pos = GetMousePosition()+ offSet;
        transform.position = BuildingSystem.currentBuilding.SnapCoordinatesToGrid(pos);
    }

}
