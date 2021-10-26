using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemInShopScript : MonoBehaviour
{
    private ItemObject itemToSell;
    private int itemPrice;
    private MoneyItem moneyBox;
    public void SetItem(ItemObject item, InventoryObject playerInv)
    {
        itemToSell = item;
    }
    public void SetItemName()
    {
        this.transform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>().text = itemToSell.name;
    }
    public void SetImage()
    {
        this.transform.GetChild(1).GetComponent<Image>().sprite = itemToSell.uiDisplay;
    }
    public void SetMoneyBox(MoneyItem MoneyBox)
    {
        moneyBox = MoneyBox;
    }
    public void SetButton(InventoryObject playerInv)
    {
        //Debug.Log("Money = " + moneyBox.moneyAmount);
        if (moneyBox.moneyAmount >= itemPrice) 
        {
            playerInv.AddItem(new Item(itemToSell), 1);
            moneyBox.moneyAmount -= itemPrice;
            return;
        }
        Debug.Log("You don't have enought RUBY");
    }
    public void SetPrice(int price)
    {
        itemPrice = price;
        this.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = itemPrice.ToString();
    }
}
