using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSetup 
{   
    public int width;
    public int height;
    private float cellSize;
    public Vector3Int originPosition;
    private TileBase tile;
    public Tilemap map;
    public int[,] gridArray;
    public GridSetup(int width, int height, float cellSize, Vector3Int originPosition, TileBase tile,Tilemap map)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        this.tile = tile;
        this.map = map;

        gridArray = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                gridArray[x, y] = 0;
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        //Debug.Log(x + ", " + y);
    }
    public void SetGround(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
        gridArray[x, y] = value;
        map.SetTile(new Vector3Int(x + originPosition.x, y + originPosition.y, -1), tile);
        //Debug.Log("SetValue");
        }
    }
    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
        return gridArray[x, y];
        } else {return -1;}
    }
    public void SetValue(int x, int y, int val)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = val;
        } else {Debug.Log("Out of grid array");}
    }
    public void GrowByOne()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (gridArray[x, y] > 0 && gridArray[x, y] < 6)
                {
                    gridArray[x, y] ++;
                    
                }
            }
        }
    }
}