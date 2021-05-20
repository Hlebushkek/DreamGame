using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySelectCell : MonoBehaviour
{
    int key = 1;
    public InventoryDisplay inventoryd;
    private float deltaToChange = 1f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HighLightInventoryCell(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HighLightInventoryCell(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HighLightInventoryCell(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            HighLightInventoryCell(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            HighLightInventoryCell(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            HighLightInventoryCell(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            HighLightInventoryCell(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            HighLightInventoryCell(8);
        }
        if (Input.mouseScrollDelta.y > deltaToChange)
        {
            HighLightInventoryCell(key - 1);
        }
        if (Input.mouseScrollDelta.y < -deltaToChange)
        {
            HighLightInventoryCell(key + 1);
        }
    }
    private void HighLightInventoryCell(int _key)
    {
        if (_key < 1 || _key > 8) return;
        key = _key;
        inventoryd.HighlightCell(_key - 1);
    }
}
