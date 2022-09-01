using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public bool placed { get; private set; }
    public Vector3Int size { get; private set; }
    public string type;
    private Vector3[] vertices;

    private void GetColliderVertexPositionLocal()
    {
        BoxCollider b  = gameObject.GetComponent<BoxCollider>();
       
        vertices = new Vector3[4];
       
        vertices[0] = b.center + new Vector3 (-b.size.x,-b.size.y, -b.size.z) * 0.5f;
        vertices[1] = b.center + new Vector3 (b.size.x,-b.size.y, -b.size.z) * 0.5f;
        vertices[2] = b.center + new Vector3 (b.size.x,-b.size.y, b.size.z) * 0.5f;
        vertices[3] = b.center + new Vector3 (-b.size.x,-b.size.y, b.size.z) * 0.5f;
    }
    
    private void CalculateSizeInCells()
    {
        Vector3Int[] VerticesInt= new Vector3Int[vertices.Length];

        for(int i = 0; i < VerticesInt.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(vertices[i]);
            VerticesInt[i] = BuildingSystem.currentBuilding.gridLayout.WorldToCell(worldPos);
        }

        size = new Vector3Int(Mathf.Abs((VerticesInt[0] - VerticesInt[1]).x), Mathf.Abs((VerticesInt[0] - VerticesInt[3]).y), 0);
    }

    
    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(vertices[0]);
    }

    private void Start()
    {
        GetColliderVertexPositionLocal();
        CalculateSizeInCells();

    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0, 45, 0));
        size = new Vector3Int(size.y, size.x, 8);
        Vector3[] Vertices = new Vector3[vertices.Length];

        for (int i = 0; i < Vertices.Length; i++)
        {
            Vertices[i] = vertices[(i + 1) % vertices.Length];

        }

        Vertices = vertices;
    }

    public virtual void Place()
    {
        ObjectDrag drag = gameObject.GetComponent<ObjectDrag>();
        Destroy(drag);

        placed = true;
    }


}
