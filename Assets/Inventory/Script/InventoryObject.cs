using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Invenotry")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDataBaseObject database;
    public Inventory Container;
    public void AddItem(Item _item, int _amount)
    {
        if (_item.buffs.Length > 0)
        {
            SetFirstEmptySlot(_item, _amount);
            return;
        }

        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].id == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }
        SetFirstEmptySlot(_item, _amount);
    }
    public InventorySlot SetFirstEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].id <= 0)
            {
                Container.Items[i].UpdateSlot(_item, _amount, _item.Id);
                return Container.Items[i];
            }
        }
        //set up func when inventory is full
        return null;
    }
    public void MoveItem(InventorySlot _item1, InventorySlot _item2)
    {
        InventorySlot temp = new InventorySlot(_item2.item, _item2.amount, _item2.id);
        _item2.UpdateSlot(_item1.item, _item1.amount, _item1.id);
        _item1.UpdateSlot(temp.item, temp.amount, temp.id);
    }

    [ContextMenu("Save")]
    public void Save(string _savePath)
    {
        /*string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter BF = new BinaryFormatter();
        FileStream dataFile = File.Create(string.Concat(Application.persistentDataPath, savePath));
        BF.Serialize(dataFile, saveData);
        dataFile.Close();*/
        IFormatter formatter = new BinaryFormatter();
        Debug.Log(Application.persistentDataPath + _savePath);
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, _savePath));
        //Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(file, Container);
        file.Close();
    }
    [ContextMenu("Load")]
    public void Load(string _savePath)
    {
        if (File.Exists(Application.persistentDataPath + _savePath))
        {
            /*BinaryFormatter BF = new BinaryFormatter();
            FileStream dataFile = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(BF.Deserialize(dataFile).ToString(), this);
            dataFile.Close();*/
            IFormatter formatter = new BinaryFormatter();
            Debug.Log(Application.persistentDataPath + _savePath);
            FileStream file = File.OpenRead(string.Concat(Application.persistentDataPath, _savePath));
            Inventory newContainer = (Inventory)formatter.Deserialize(file);
            for (int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].UpdateSlot(newContainer.Items[i].item, newContainer.Items[i].amount, newContainer.Items[i].id);
            }
            file.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }
    public ItemType GetItemType(int i)
    {
        if (i >= Container.Items.Length)
        {
            return ItemType.Empty;
        }
        else return Container.Items[i].item.type;
    }
    public Item GetItem(int i)
    {
        if (i >= Container.Items.Length)
        {
            return null;
        }
        else return Container.Items[i].item;
    }
    public float GetItemDamage(int i)
    {
        if (i >= Container.Items.Length)
        {
            return 0;
        }
        else 
        {
            for (int j = 0; j < Container.Items[i].item.buffs.Length; j++)
                if (Container.Items[i].item.buffs[j].attribute == ItemAttributes.Damage)
                    return Container.Items[i].item.buffs[j].value;
                        
            return 0;
        }
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[24];
}
[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int amount;
    public int id;
    public InventorySlot()
    {
        item = null;
        amount = 0;
        id = 0;
    }
    public InventorySlot(Item _item, int _amount, int _id)
    {
        item = _item;
        amount = _amount;
        id = _id;
    }
    public void UpdateSlot(Item _item, int _amount, int _id)
    {
        item = _item;
        amount = _amount;
        id = _id;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
    public void ReduceAmount(int value)
    {
        amount -= value;
        if (amount <= 0) DeleteItem();
    }
    public void DeleteItem()
    {
        item = new Item();
        amount = 0;
        id = 0;
    }
}
