using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInSpaceShipScript : MonoBehaviour
{
    [SerializeField] private GameObject ShopUIPrefab;
    [SerializeField] private GameObject PlayerInventory;
    private void Start()
    {
        ShopUIPrefab.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            PlayerInventory.SetActive(false);
            ShopUIPrefab.SetActive(true);
            ShopUIPrefab.GetComponent<BuyScript>().DrawSellInventory();
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        PlayerInventory.SetActive(true);
        ShopUIPrefab.SetActive(false);
        ShopUIPrefab.GetComponent<BuyScript>().DeleteSellInventory();
    }
}
