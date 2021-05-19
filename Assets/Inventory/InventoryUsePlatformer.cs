using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUsePlatformer : MonoBehaviour
{
    [SerializeField]
    private InventoryDisplay invD;
    [SerializeField]
    private GameObject Character;
    private int CurrentlySelectedCell = 0;
    private void Start()
    {
        CurrentlySelectedCell = invD.GetHiglightedCell();
        Debug.Log("Current cell = " + CurrentlySelectedCell);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CurrentlySelectedCell = invD.GetHiglightedCell();
            WhatIsUsedPlatformer(CurrentlySelectedCell);
        }
    }
    private void WhatIsUsedPlatformer(int cellIndex)
    {
        ItemType type;
        if (cellIndex >= 0 && cellIndex <= 6)
        {
            type = invD.inventory.GetItemType(cellIndex);
            Debug.Log("Type of selected item = " + type.ToString());
            switch(type)
            {
            case ItemType.Sword:
                Debug.Log(invD.inventory.GetItemDamage(cellIndex));
                Character.GetComponent<HeroAttackScript>().Attack(invD.inventory.GetItemDamage(cellIndex));
                break;
            default :
                Debug.Log("Can't use item of type " + type.ToString());
                break;
            }
        }
    }
}
