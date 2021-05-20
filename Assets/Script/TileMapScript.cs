using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileMapScript : MonoBehaviour
{
    public GridSetup grid;
    public Tilemap map;
    public TileBase plantTile;
    public int gridWidth, gridHeight, gridSizeX16;
    private int x, y, x1, y1;
    public Vector3Int gridOrigin;
    public GameObject Girl;
    [SerializeField] CropDataBase cropDataBase;
    private Dictionary<Vector2Int, GameObject> CoordToPlant = new Dictionary<Vector2Int, GameObject>();
    private void Start()
    {
        grid = new GridSetup(gridWidth, gridHeight, gridSizeX16, gridOrigin, plantTile, map);
    }
    public void DecideWhatToDo()
    {
        grid.GetXY(GetMouseWorldPosition(Input.mousePosition, Camera.main), out x, out y);
        Debug.Log(x + " ::: " + y);
        if (grid.GetValue(x, y) == 0) 
        {
            SetGroundToPlantCrop();
            return;
        }
        if (grid.GetValue(x, y) == 2 && IsPlantGrow(new Vector2Int(x, y)))
        {
            Debug.Log("Collect Plant");
            CollectPlant();
            return;
        }

    }
    public void CollectPlant()
    {
        CoordToPlant[new Vector2Int(x, y)].GetComponent<CropGrow>().CollectCrop();
        Destroy(CoordToPlant[new Vector2Int(x, y)]);
        CoordToPlant.Remove(new Vector2Int(x, y));
    }
    public void SetGroundToPlantCrop()
    {
        if(Mathf.Abs(GetMouseWorldPosition(Input.mousePosition, Camera.main).x - Girl.transform.position.x) < 2 && Mathf.Abs(GetMouseWorldPosition(Input.mousePosition, Camera.main).y - Girl.transform.position.y) < 2)
        {
            Girl.SendMessage("HoeAnimStart");
            Invoke("SetGround", 1.4f);
            Invoke("SetMessageAnimEnd", 1.5f);
        }
    }
    public bool PlantSeed(Item item)
    {
        grid.GetXY(GetMouseWorldPosition(Input.mousePosition, Camera.main), out x, out y);
        if(Mathf.Abs(GetMouseWorldPosition(Input.mousePosition, Camera.main).x - Girl.transform.position.x) < 2 && Mathf.Abs(GetMouseWorldPosition(Input.mousePosition, Camera.main).y - Girl.transform.position.y) < 2 && grid.GetValue(x, y) == 1)
        {
            Girl.SendMessage("HoeAnimStart");
            SetPlant(item);
            Invoke("SetMessageAnimEnd", 1.5f);
            return true;
        } else return false;
    }
    private void SetGround()
    {
        grid.SetGround(x, y, 1);
    }
    private void SetPlant(Item item)
    {
        //Debug.Log("Plant seed with ID = " + item.Id);
        //Debug.Log("Item name = " + item.Name);
        //Debug.Log("Name of obj " + cropDataBase.GetCropObject[item.Id].name);
        GameObject newPlant = Instantiate(cropDataBase.GetCropObject[item.Id], new Vector3(x + gridOrigin.x + 0.5f, y + gridOrigin.y + 0.75f, 0), Quaternion.identity) as GameObject;
        newPlant.transform.SetParent(this.transform, false);
        grid.SetValue(x, y, 2);
        CoordToPlant.Add(new Vector2Int(x, y), newPlant);
    }
    private bool IsPlantGrow(Vector2Int coord)
    {
        return CoordToPlant[coord].GetComponent<CropGrow>().IsCropGrow();
    }
    private void SetMessageAnimEnd()
    {
        Girl.SendMessage("HoeAnimEnd");
    }
    public static Vector3 GetMouseWorldPosition(Vector3 screenPosition, Camera camera)
    {
        Vector3 mousePosition = camera.ScreenToWorldPoint(screenPosition);
        return mousePosition;
    }
    public void Grow()
    {
        BroadcastMessage("GrowByOne", SendMessageOptions.DontRequireReceiver);
    }
}
