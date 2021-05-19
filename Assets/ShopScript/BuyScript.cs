using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyScript : MonoBehaviour
{
    [SerializeField] ItemObject[] ItemsInShop;
    [SerializeField] int[] itemPrice;
    [SerializeField] InventoryObject playerInventory;
    [SerializeField] GameObject Shop_BuyPanel;
    [SerializeField] GameObject SellInventory;
    [SerializeField] GameObject ScrollaleContent;
    [SerializeField] MoneyItem MoneyBox;
    [SerializeField] float X_Start, Y_Start, Y_SpaceBetweenItems;
    private GameObject OnDrawSellInventory;
    private void Start()
    {
        for (int i = 0; i < ItemsInShop.Length; i++)
        {
            //Debug.Log("Instantiate itempanel");
            var obj = Instantiate(Shop_BuyPanel);
            obj.transform.SetParent(ScrollaleContent.transform, false);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponent<BuyItemInShopScript>().SetItem(ItemsInShop[i], playerInventory);
            obj.GetComponent<BuyItemInShopScript>().SetImage();
            obj.GetComponent<BuyItemInShopScript>().SetMoneyBox(MoneyBox);
            obj.GetComponent<BuyItemInShopScript>().SetPrice(itemPrice[i]);
            obj.GetComponent<BuyItemInShopScript>().SetItemName();
        }

    }
    public void DrawSellInventory()
    {
        if (OnDrawSellInventory == null)
        {
            OnDrawSellInventory = Instantiate(SellInventory);
            OnDrawSellInventory.transform.SetParent(this.transform, false);
            OnDrawSellInventory.GetComponent<RectTransform>().localPosition = new Vector3(0, -54);
        }  
    }
    public void DeleteSellInventory()
    {
        Destroy(OnDrawSellInventory);
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_Start, Y_Start + (-Y_SpaceBetweenItems) * i, 0f);
    }
}
