using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Realtime;
using Photon.Pun;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem currentBuilding;
    private PlaceableObject objectoToPlace;

    [Header("Grid and Tile Settings")]
    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap mainTile;
    [SerializeField] private TileBase selectTile;
    

    [Header("Buildings ")]
    [SerializeField] GameObject[] buildings;
    //public GameObject building2;
    void Awake()
    {
        currentBuilding = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
 
    }

    public void PlaceObject()
    {
        if (!objectoToPlace)
        {
            return;
        }

        if (CanBePlaced(objectoToPlace))
        {

            objectoToPlace.Place();
            Vector3Int start = gridLayout.WorldToCell(objectoToPlace.GetStartPosition());
 
            TakeArea(start, objectoToPlace.size);
            Debug.Log("placed");

            PhotonNetwork.Instantiate(objectoToPlace.type,objectoToPlace.transform.position, objectoToPlace.transform.rotation);

            Destroy(objectoToPlace.gameObject);
            CameraControllerNetWork.instance.CenterPlayer();
            UIController.instance.joystick.gameObject.SetActive(true);
            UIController.instance.StopBuilding();

            UIController.instance.pickBtn.onClick.RemoveListener(PlaceObject);

            

        }

    }

    public void  OnClickBuilding(int num)
    {
        UIController.instance.pickBtn.onClick.AddListener(PlaceObject);
        InitializeWithObject(buildings[num]);
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
      
            Vector3 positon = SnapCoordinatesToGrid(grid.transform.position);
        
            GameObject obj = Instantiate(prefab, positon, Quaternion.identity);
        UIController.instance.joystick.gameObject.SetActive(false);
        UIController.instance.StartBuilding();
        UIController.instance.cancel.onClick.AddListener(Cancel);
        UIController.instance.rotate.onClick.AddListener(Rotate);
        CameraControllerNetWork.instance.ChangeTarget(obj.transform);
        objectoToPlace = obj.GetComponent<PlaceableObject>();

        // obj.AddComponent<ObjectDrag>();

    }


    public void Cancel()
    {
        Destroy(objectoToPlace.gameObject);
        CameraControllerNetWork.instance.CenterPlayer();
        UIController.instance.joystick.gameObject.SetActive(true);
        UIController.instance.StopBuilding();

        UIController.instance.pickBtn.onClick.RemoveListener(PlaceObject);
    }
    public void Rotate()
    {
       objectoToPlace.Rotate();
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
