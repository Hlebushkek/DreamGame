using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crop DataBase", menuName = "Inventory System/Items/CropDataBase")]
public class CropDataBase : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private int[] cropItemId;
    [SerializeField] private GameObject[] CropObj;
    public Dictionary<int, GameObject> GetCropObject = new Dictionary<int, GameObject>();  
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < cropItemId.Length; i++)
        {
            //Debug.Log(cropItemId[i]);
            GetCropObject.Add(cropItemId[i], CropObj[i]);
        }
    }
    public void OnBeforeSerialize()
    {
        GetCropObject = new Dictionary<int, GameObject>();
    }
}
