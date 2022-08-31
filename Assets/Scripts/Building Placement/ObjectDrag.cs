using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offSet;
    public LayerMask BuildingZone;
    // static  BuildingSystem buildingSystem;

    public Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 1000f, BuildingZone))
        {
            return raycastHit.point;
        }
        else return Vector3.zero;
    }
    private void OnMouseDown()
    {
        offSet = transform.position - GetMousePosition();
    }

    private void OnMouseDrag()
    {
        Vector3 pos = GetMousePosition()+ offSet;
        transform.position = BuildingSystem.currentBuilding.SnapCoordinatesToGrid(pos);
    }

}
