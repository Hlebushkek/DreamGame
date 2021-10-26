using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Gun DataBase", menuName = "Inventory System/Items/GunDataBase")]
public class GunDataBase : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private int[] GunItemId;
    [SerializeField] private GameObject[] BulletObj;
    public Dictionary<int, GameObject> GetBulletObject = new Dictionary<int, GameObject>();  
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < GunItemId.Length; i++)
        {
            //Debug.Log(cropItemId[i]);
            GetBulletObject.Add(GunItemId[i], BulletObj[i]);
        }
    }
    public void OnBeforeSerialize()
    {
        GetBulletObject = new Dictionary<int, GameObject>();
    }
}
