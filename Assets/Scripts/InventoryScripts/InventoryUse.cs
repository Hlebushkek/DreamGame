using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUse : MonoBehaviour
{
    [SerializeField] private InventoryDisplay invD;
    [SerializeField] private TileMapScript tms;
    [SerializeField] private GunDataBase gunData;
    private int CurrentlySelectedCell = 0;
    private float cooldownTimer = 0f;
    private void Start()
    {
        CurrentlySelectedCell = invD.GetHiglightedCell();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && cooldownTimer <= 0f)
        {
            CurrentlySelectedCell = invD.GetHiglightedCell();
            Debug.Log("Current cell = " + CurrentlySelectedCell);
            cooldownTimer = WhatIsUsed(CurrentlySelectedCell);
        }
        cooldownTimer -= Time.fixedDeltaTime;
    }

    public float WhatIsUsed(int i)
    {
        Item item;
        ItemType type;
        if (i >= 0 && i <=7)
        {
            item = invD.inventory.GetItem(i);
            type = invD.inventory.GetItemType(i);
            Debug.Log("Type of selected item = " + type.ToString());
            switch (type)
            {
            case ItemType.Hoe:
                tms.DecideWhatToDo();
                return 1.75f;
            case ItemType.CropSeed:
                if (tms.PlantSeed(item))
                    invD.inventory.Container.Items[i].ReduceAmount(1);
                return 1.75f;
            case ItemType.Gun:
                Debug.Log("Gun");
                return 0.5f;
            default:
                Debug.Log("Default case");
                break;
            }
        }
        return 0.1f;
    }
}
