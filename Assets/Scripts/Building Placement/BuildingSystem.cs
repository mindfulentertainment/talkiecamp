using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem currentBuilding;
    private PlaceableObject objectoToPlace;

    [Header("Grid and Tile Settings")]
    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap mainTile;
    [SerializeField] private TileBase selectTile;
    [SerializeField] LayerMask BuildingZone;

    [Header("Buildings ")]
    [SerializeField] GameObject[] buildings;
    //public GameObject building2;

    


    void Awake()
    {
        currentBuilding = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();  
    }

      void Update()
    {


                if (!objectoToPlace)
                {
                    return;
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    objectoToPlace.Rotate();
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {

                    if (CanBePlaced(objectoToPlace))
                    {

                        objectoToPlace.Place();
                        Vector3Int start = gridLayout.WorldToCell(objectoToPlace.GetStartPosition());
                        print(start);
                        TakeArea(start, objectoToPlace.size);
                        mainTile.color = Color.red;
                        Debug.Log("placed");
                        objectoToPlace.gameObject.tag = ("Placed");


                    }
                    else
                    {
                        Destroy(objectoToPlace.gameObject);
                        Debug.Log("destroy");
                    }

                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Destroy(objectoToPlace.gameObject);
                }

               
            
        
    }

    

    public void  OnClickBuilding(int num)
    {
        InitializeWithObject(buildings[num]);
       
    }

  

    public static  Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else return Vector3.zero;
    }

    public Vector3 SnapCoordinatesToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

     private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach(var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        return array;   
    }

    public void InitializeWithObject(GameObject prefab)
    {
      
            Vector3 positon = SnapCoordinatesToGrid(Vector3.zero);
        
            GameObject obj = Instantiate(prefab, positon, Quaternion.identity);
            objectoToPlace = obj.GetComponent<PlaceableObject>();
            obj.AddComponent<ObjectDrag>();
        
    }

    private bool CanBePlaced (PlaceableObject placeableObject)
    {
        BoundsInt area = new BoundsInt();   

        area.position = gridLayout.WorldToCell(objectoToPlace.GetStartPosition());
        area.size = placeableObject.size;

        TileBase[] baseArray = GetTilesBlock(area, mainTile);

        foreach( var b in baseArray)
        {
            if (b == selectTile)
            {
                return false;
            }
        }

        return true;
    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        mainTile.BoxFill(start,selectTile,start.x,start.y,start.x+size.x,start.y+size.y);
        
      
    }

}
