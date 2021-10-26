using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    CropSeed,
    CropProduct,
    Hoe,
    Sword,
    Gun,
    Potion,
    Ore,
    Helmet,
    Chestplate,
    Leggings,
    Boots,
    Default,
    Empty,
}
public enum ItemAttributes
{
    Damage,
    AttackRate,
    Durability,
}
public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public int itemPriceInJustMoney;
    public Sprite uiDisplay;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
    public ItemBuffs[] buffs;
    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}
[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public int itemPriceInJustMoney;
    public ItemBuffs[] buffs;
    public ItemType type;
    public Item()
    {
        Name = "";
        Id = 0;
        itemPriceInJustMoney = 0;
        buffs = null;
        type = ItemType.Default;
    }
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
        type = item.type;
        if (item.buffs.Length > 0) 
        {
            buffs = new ItemBuffs[item.buffs.Length];
            for (int i = 0; i < buffs.Length; i++)
            {
                buffs[i] = new ItemBuffs(item.buffs[i].value, item.buffs[i].attribute);
            }
        }
        else
        {
            buffs = new ItemBuffs[0];
        }
        itemPriceInJustMoney = item.itemPriceInJustMoney;
    }
}
[System.Serializable]
public class ItemBuffs
{
    public ItemAttributes attribute;
    public float value;
    public ItemBuffs(float value, ItemAttributes attribute)
    {
        this.attribute = attribute;
        this.value = value;
    }
}