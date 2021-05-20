using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{
    [SerializeField] private InventoryObject inventory;
    [SerializeField] private InventoryObject equipmentInventory;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Save");
            inventory.Save("/Inventory.save");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Load");
            inventory.Load("/Inventory.save");
        }
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[24];
        equipmentInventory.Container.Items = new InventorySlot[10];
    }
}
