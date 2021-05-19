using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplaySellItemInventory : MonoBehaviour
{
    [SerializeField] InventoryObject PlayerInventory;
    [SerializeField] GameObject InventorySlot; //Slot with sell option
    [SerializeField] MoneyItem MoneyBox;
    [SerializeField] private int X_Start, Y_Start, X_Between, Y_Between;
    private Dictionary<GameObject, InventorySlot> SellSlotToInventorySlot = new Dictionary<GameObject, InventorySlot>();
    private GameObject[] SellSlot;
    private void Start()
    {
        SellSlot = new GameObject[PlayerInventory.Container.Items.Length];
        for (int i = 0, j = 0; i < PlayerInventory.Container.Items.Length; i++)
        {
            if (PlayerInventory.Container.Items[i].id > 0)
            {
                var obj = Instantiate(InventorySlot);
                obj.transform.SetParent(this.transform, false);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(j);
                obj.transform.GetChild(0).GetComponent<Image>().sprite = PlayerInventory.database.GetItemObject[PlayerInventory.Container.Items[i].item.Id].uiDisplay;
                obj.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = PlayerInventory.Container.Items[i].amount.ToString();
                int x = new int();
                x = i;
                obj.GetComponent<Button>().onClick.AddListener( delegate {SellItem(x);} );
                SellSlot[i] = obj;
                SellSlotToInventorySlot.Add(obj, PlayerInventory.Container.Items[i]);
                j++;
            }
        }
    }
    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_Start + (i % 8) * X_Between, Y_Start - (int)Mathf.Floor(i/8) * Y_Between);
    }
    public void SellItem(int i)
    {
        //Debug.Log("i =" + i);
        InventorySlot currentSlot = SellSlotToInventorySlot[SellSlot[i]];
        MoneyBox.moneyAmount += currentSlot.item.itemPriceInJustMoney;
        currentSlot.ReduceAmount(1);
        if (currentSlot.amount <= 0) DeletEmptySellSlot(i);
        SellSlot[i].transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = PlayerInventory.Container.Items[i].amount.ToString();
    }
    private void DeletEmptySellSlot(int i)
    {
        SellSlotToInventorySlot.Remove(SellSlot[i]);
        Destroy(SellSlot[i]);
    }
}
