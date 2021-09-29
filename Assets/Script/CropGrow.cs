using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropGrow : MonoBehaviour
{
    [SerializeField] private InventoryObject PlayerInventory;
    [SerializeField] private int randomMin, randomMax;
    public ItemObject productionItem;
    public Sprite[] CropState;
    private int state = 0;
    private void GrowByOne()
    {
        if (state < CropState.Length - 1)
        {
            Debug.Log("GrowByOne");
            state++;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = CropState[state];
        } else {Debug.Log("Already grow");}
    }
    public void CollectCrop()
    {
        int amount = Random.Range(randomMin, randomMax);
        PlayerInventory.AddItem(new Item(productionItem), amount);
    }
    public bool IsCropGrow() {return state == CropState.Length - 1;}
    public int GetCropState() {return state;}
}
