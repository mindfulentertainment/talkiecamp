using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offSet;

    private void OnMouseDown()
    {
        offSet = transform.position - BuildingSystem.GetMousePosition();
    }

    private void OnMouseDrag()
    {
        Vector3 pos = BuildingSystem.GetMousePosition()+ offSet;
        transform.position = BuildingSystem.currentBuilding.SnapCoordinatesToGrid(pos);
    }

}
