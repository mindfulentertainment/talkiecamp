using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offSet;
    private Vector3 lastPoint;
    public LayerMask BuildingZone;
    // static  BuildingSystem buildingSystem;

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

    private void OnMouseDrag()
    {
        Vector3 pos = GetMousePosition()+ offSet;
        transform.position = BuildingSystem.currentBuilding.SnapCoordinatesToGrid(pos);
    }

}
