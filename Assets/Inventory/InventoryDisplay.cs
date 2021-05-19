using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InventoryDisplay : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private GameObject inventorySmallPanel;
    [SerializeField] private GameObject inventoryFullPanel;
    [SerializeField] private GameObject inventoryEquipmentSlots;
    [SerializeField] private Image inventorySlotImage;
    [SerializeField] private int X_SpaceBetweenItems;
    [SerializeField] private int Y_SpaceBetweenItems;
    [SerializeField] private int N_OfBlocksInRow;
    [SerializeField] private int N_OfBlocksInColumn;
    [SerializeField] private int X_Start, X_Start_OfFull;
    [SerializeField] private int Y_Start, Y_StartFull;
    [SerializeField] private ItemType[] AllowedType;
    public InventoryObject inventory;
    public InventoryObject equipmentInv;
    private int CurrentlyHighlightedCell;
    private Image[] inventorySlotAr;
    private Dictionary<GameObject, InventorySlot> itemsDisplay = new Dictionary<GameObject, InventorySlot>();
    private Dictionary<GameObject, InventorySlot> itemsSmallPanelDisplay = new Dictionary<GameObject, InventorySlot>();
    private Dictionary<GameObject, InventorySlot> equipmentDisplay = new Dictionary<GameObject, InventorySlot>();
    private void Start()
    {
        inventorySlotAr = new Image[N_OfBlocksInRow];
        CreateSlots();
        inventoryFullPanel.SetActive(false);
        HighlightCell(0);
    }
    private void Update()
    {
        UpdateSlots();
        if (Input.GetKeyDown(KeyCode.I)) UseFullInventory();
    }
    /*private void CreateDisplay()
    {
        /*Debug.Log("Create inventory Display");
        for (int i = 0; i < N_OfBlocksInRow; i++) //Invsantiate Bottom Inventory Panel
        {
            inventorySlotAr[i] = Instantiate(inventorySlotImage);
            inventorySlotAr[i].transform.SetParent(inventorySmallPanel.transform.GetChild(0).transform);
            inventorySlotAr[i].GetComponent<RectTransform>().localPosition = GetPosition(i);
        }

        for (int i = 0; i < N_OfBlocksInColumn; i++) //Instantiate Big Inventory Panel
        {
            for (int j = 0; j < N_OfBlocksInRow; j++)
            {
                var obj = Instantiate(inventorySlotImage);
                obj.transform.SetParent(inventoryFullPanel.transform.GetChild(0).transform);
                obj.GetComponent<RectTransform>().localPosition = GetPositionOfFull(j, i);
            }
        }


        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];

            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponentInChildren<Image>().sprite = inventory.database.GetItemObject[slot.item.Id].uiDisplay;
            obj.transform.SetParent(inventorySmallPanel.transform.GetChild(0).transform);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            if (i < N_OfBlocksInRow)
            {   
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

                var obj1 = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj1.GetComponentInChildren<Image>().sprite = inventory.database.GetItemObject[slot.item.Id].uiDisplay;
                obj1.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                obj1.transform.SetParent(inventoryFullPanel.transform.GetChild(0).transform);
                obj1.GetComponent<RectTransform>().localPosition = GetPositionOfFull(i % N_OfBlocksInRow, (int)Mathf.Floor(i/N_OfBlocksInRow));
                itemsDisplayCopyOfFirstRow.Add(slot, obj1);
            }
            else 
            {
                obj.transform.SetParent(inventoryFullPanel.transform.GetChild(0).transform);
                obj.GetComponent<RectTransform>().localPosition = GetPositionOfFull(i % N_OfBlocksInRow, (int)Mathf.Floor(i/N_OfBlocksInRow));
            }

            itemsDisplay.Add(slot, obj);
        }
        HighlightCell(0);
    }*/
    /*public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];
            
            if (itemsDisplay.ContainsKey(slot))
            {
                itemsDisplay[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                if (i < N_OfBlocksInRow)itemsDisplayCopyOfFirstRow[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<Image>().sprite = inventory.database.GetItemObject[slot.item.Id].uiDisplay;
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                obj.transform.SetParent(inventorySmallPanel.transform.GetChild(0).transform);

                if (i < N_OfBlocksInRow)
                {
                    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                    var obj1 = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                    obj1.GetComponentInChildren<Image>().sprite = inventory.database.GetItemObject[slot.item.Id].uiDisplay;
                    obj1.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                    obj1.transform.SetParent(inventoryFullPanel.transform.GetChild(0).transform);
                    obj1.GetComponent<RectTransform>().localPosition = GetPositionOfFull(i % N_OfBlocksInRow, (int)Mathf.Floor(i/N_OfBlocksInRow));
                    itemsDisplayCopyOfFirstRow.Add(slot, obj1);

                    AddEvent(obj1, EventTriggerType.PointerEnter, delegate { OnEnter(obj1); });
                    AddEvent(obj1, EventTriggerType.PointerExit, delegate { OnExit(obj1); });
                    AddEvent(obj1, EventTriggerType.BeginDrag, delegate { OnStartDrag(obj1); });
                    AddEvent(obj1, EventTriggerType.EndDrag, delegate { OnEndDrag(obj1); });
                    AddEvent(obj1, EventTriggerType.Drag, delegate { OnDrag(obj1); });
                }
                else 
                {
                    obj.transform.SetParent(inventoryFullPanel.transform.GetChild(0).transform);
                    obj.GetComponent<RectTransform>().localPosition = GetPositionOfFull(i % N_OfBlocksInRow, (int)Mathf.Floor(i/N_OfBlocksInRow));
                }
                AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
                AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
                AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnStartDrag(obj); });
                AddEvent(obj, EventTriggerType.EndDrag, delegate { OnEndDrag(obj); });
                AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

                itemsDisplay.Add(slot, obj);
            }
            if (slot.amount <= 0) DeleteSlot(i);
        }
    }*/
    private void CreateSlots()
    {
        itemsDisplay = new Dictionary<GameObject, InventorySlot>();
        itemsSmallPanelDisplay = new Dictionary<GameObject, InventorySlot>();
        equipmentDisplay = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++) //Instantiate slot for Full inventory
        {
            var obj = Instantiate(inventorySlotImage, Vector3.zero, Quaternion.identity, transform);
            obj.transform.SetParent(inventoryFullPanel.transform.GetChild(0).transform);
            obj.GetComponent<RectTransform>().localPosition = GetPositionOfFull(i % N_OfBlocksInRow, Mathf.FloorToInt(i / N_OfBlocksInRow));
        }
        for (int i = 0; i < N_OfBlocksInRow; i++) //Instantiate 8 slot for small inventory panel
        {
            var obj = Instantiate(inventorySlotImage, Vector3.zero, Quaternion.identity, transform);
            obj.transform.SetParent(inventorySmallPanel.transform.GetChild(0).transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            inventorySlotAr[i] = obj;
        }
        for (int i = 0; i < inventory.Container.Items.Length; i++) //Instantiate slot with items
        {
            var obj = Instantiate(inventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.SetParent(inventoryFullPanel.transform.GetChild(0).transform);
            obj.GetComponent<RectTransform>().localPosition = GetPositionOfFull(i % N_OfBlocksInRow, Mathf.FloorToInt(i / N_OfBlocksInRow));
            
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            itemsDisplay.Add(obj, inventory.Container.Items[i]);
        }

        for (int i = 0; i < N_OfBlocksInRow; i++) //Instantiate 8 slot with items for small panel
        {
            var obj = Instantiate(inventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.SetParent(inventorySmallPanel.transform.GetChild(0).transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            itemsSmallPanelDisplay.Add(obj, inventory.Container.Items[i]);
        }
    
        for (int i = 0; i < 10; i++) //Instantiate slot with items
        {
            var obj = Instantiate(inventorySlotPrefab, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(inventoryEquipmentSlots.transform, false);
            obj.GetComponent<RectTransform>().localPosition = inventoryEquipmentSlots.transform.GetChild(i).localPosition;
            obj.AddComponent<AllowedItemType>().AllowedType = AllowedType[i];

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            equipmentDisplay.Add(obj, equipmentInv.Container.Items[i]);
        }
    }
    private void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplay)
        {
            if (_slot.Value.id > 0)
            {
                _slot.Key.GetComponent<Image>().sprite = inventory.database.GetItemObject[_slot.Value.item.Id].uiDisplay;
                _slot.Key.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "1" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.GetComponent<Image>().sprite = null;
                _slot.Key.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsSmallPanelDisplay)
        {
            if (_slot.Value.id > 0)
            {
                _slot.Key.GetComponent<Image>().sprite = inventory.database.GetItemObject[_slot.Value.item.Id].uiDisplay;
                _slot.Key.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "1" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.GetComponent<Image>().sprite = null;
                _slot.Key.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in equipmentDisplay)
        {
            if (_slot.Value.id > 0)
            {
                _slot.Key.GetComponent<Image>().sprite = equipmentInv.database.GetItemObject[_slot.Value.item.Id].uiDisplay;
                _slot.Key.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "1" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.GetComponent<Image>().sprite = null;
                _slot.Key.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_Start + (X_SpaceBetweenItems * i), Y_Start, 0f);
    }
    public Vector3 GetPositionOfFull(int i, int row)
    {
        return new Vector3(X_Start_OfFull + (X_SpaceBetweenItems * i), Y_StartFull + (-Y_SpaceBetweenItems) * row, 0f);
    }
    public void HighlightCell(int i)
    {
        //Make another cell standar color
        for (int j = 0; j < N_OfBlocksInRow; j++)
        {
            inventorySlotAr[j].color = Color.white;
        }
        //Highlight cell "i"
        inventorySlotAr[i].color = Color.red;
        CurrentlyHighlightedCell = i;
    }
    public int GetHiglightedCell()
    {
        return CurrentlyHighlightedCell;
    }
    private void UseFullInventory()
    {
        if(inventoryFullPanel.activeSelf)
        {
            inventorySmallPanel.SetActive(true);
            inventoryFullPanel.SetActive(false);
        }
        else
        {
            inventoryFullPanel.SetActive(true);
            inventorySmallPanel.SetActive(false);
        }
    }
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    public void OnEnter(GameObject obj)
    {
        mouseItem.hoverobj = obj;
        if (itemsDisplay.ContainsKey(obj))
        {
            mouseItem.hoverItem = itemsDisplay[obj];
            mouseItem.typeTo = 0;
        }
        else if (equipmentDisplay.ContainsKey(obj))
        {
            mouseItem.hoverItem = equipmentDisplay[obj];
            mouseItem.typeTo = 1;
        }
    }
    public void OnExit(GameObject obj)
    {
        mouseItem.hoverobj = null;
        mouseItem.hoverItem = null;
    }
    public void OnDragStart(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(1, 1);
        mouseObject.transform.SetParent(transform.parent);
        if (itemsDisplay.ContainsKey(obj) && itemsDisplay[obj].id >= 1)
        {
            Debug.Log("is not default item");
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItemObject[itemsDisplay[obj].id].uiDisplay;
            img.raycastTarget = false;
            mouseItem.item = itemsDisplay[obj];
            mouseItem.typeFrom = 0;
        }
        else if (equipmentDisplay.ContainsKey(obj) && equipmentDisplay[obj].id >= 1)
        {
            Debug.Log("is not default item");
            var img = mouseObject.AddComponent<Image>();
            img.sprite = equipmentInv.database.GetItemObject[equipmentDisplay[obj].id].uiDisplay;
            img.raycastTarget = false;
            mouseItem.item = equipmentDisplay[obj];
            mouseItem.typeFrom = 1;
        }
        mouseItem.obj = mouseObject;
    }
    public void OnDragEnd(GameObject obj)
    {
        if (mouseItem.hoverobj && mouseItem.typeFrom == 0 && mouseItem.typeTo == 0)
        {
            inventory.MoveItem(itemsDisplay[obj], itemsDisplay[mouseItem.hoverobj]);
        }
        else if (mouseItem.hoverobj && mouseItem.typeFrom == 0 && mouseItem.typeTo == 1 && (itemsDisplay[obj].item.type == mouseItem.hoverobj.GetComponent<AllowedItemType>().AllowedType))
        {
            inventory.MoveItem(itemsDisplay[obj], equipmentDisplay[mouseItem.hoverobj]);
        }
        else if (mouseItem.hoverobj && mouseItem.typeFrom == 1 && mouseItem.typeTo == 0)
        {
            inventory.MoveItem(equipmentDisplay[obj], itemsDisplay[mouseItem.hoverobj]);
        }
        else if (mouseItem.hoverobj && mouseItem.typeFrom == 1 && mouseItem.typeTo == 1 && (equipmentDisplay[obj].item.type == mouseItem.hoverobj.GetComponent<AllowedItemType>().AllowedType))
        {
            inventory.MoveItem(equipmentDisplay[obj], equipmentDisplay[mouseItem.hoverobj]);
        }
        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }
    public void OnDrag(GameObject obj)
    {
        if (mouseItem.obj != null)
            mouseItem.obj.GetComponent<RectTransform>().position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -6f);
    }
}
public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverobj;
    public int typeFrom; //0 - regular ; 1 - equipment
    public int typeTo; //0 - regular; 1 - equipment
}

