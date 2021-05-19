using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
    MouseItem mouseItem = new MouseItem();
    [SerializeField] private InventoryObject chestInv;
    [SerializeField] private GameObject Player;
    [SerializeField] private InventoryObject playerInv;
    [SerializeField] private GameObject ChestCanvas;
    [SerializeField] private GameObject PlayerInventory;
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private Transform ChestPanel;
    [SerializeField] private Transform PlayerInvPanel;
    private Dictionary<GameObject, InventorySlot> chestDisplay = new Dictionary<GameObject, InventorySlot>();
    private Dictionary<GameObject, InventorySlot> itemsDisplay = new Dictionary<GameObject, InventorySlot>();
    private void Start()
    {
        chestInv = ScriptableObject.CreateInstance<InventoryObject>();
        chestInv.database = playerInv.database;
        chestInv.Container = new Inventory();
        for (int i = 0; i < chestInv.Container.Items.Length; i++) chestInv.Container.Items[i] = new InventorySlot();
        Player = GameObject.FindGameObjectWithTag("Player");
        CreateItemSlots();
        UpdateSlots();
        ChestCanvas.SetActive(false);
    }
    private void Update()
    {
        UpdateSlots();
    }
    private void OnMouseDown()
    {
        if (Vector2.Distance(Player.transform.position, this.transform.position) < 2)
            EnableChestInv();
    }
    private void EnableChestInv()
    {
        Debug.Log("clicked");
        ChestCanvas.SetActive(true);
        PlayerInventory.SetActive(false);
    }
    public void CloseChestInv()
    {
        ChestCanvas.SetActive(false);
        PlayerInventory.SetActive(true);
    }
    private void CreateItemSlots()
    {
        for (int i = 0; i < chestInv.Container.Items.Length; i++) //Instantiate slot with items
        {
            var obj = Instantiate(inventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.SetParent(ChestPanel.GetChild(1).transform, false);
            obj.GetComponent<RectTransform>().localPosition = ChestPanel.GetChild(0).GetChild(i).GetComponent<RectTransform>().localPosition;
            
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            
            chestDisplay.Add(obj, chestInv.Container.Items[i]);
        }
        for (int i = 0; i < playerInv.Container.Items.Length; i++) //Instantiate slot with items
        {
            var obj = Instantiate(inventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.SetParent(PlayerInvPanel.GetChild(1).transform, false);
            obj.GetComponent<RectTransform>().localPosition = PlayerInvPanel.GetChild(0).GetChild(i).GetComponent<RectTransform>().localPosition;
            
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            itemsDisplay.Add(obj, playerInv.Container.Items[i]);
        }
    }
    private void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplay)
        {
            if (_slot.Value.id > 0)
            {
                _slot.Key.GetComponent<Image>().sprite = playerInv.database.GetItemObject[_slot.Value.item.Id].uiDisplay;
                _slot.Key.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.transform.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "1" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.GetComponent<Image>().sprite = null;
                _slot.Key.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.transform.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
            }
        }
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in chestDisplay)
        {
            if (_slot.Value.id > 0)
            {
                _slot.Key.GetComponent<Image>().sprite = chestInv.database.GetItemObject[_slot.Value.item.Id].uiDisplay;
                _slot.Key.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.transform.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "1" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.GetComponent<Image>().sprite = null;
                _slot.Key.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.transform.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
            }
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
        else if (chestDisplay.ContainsKey(obj))
        {
            mouseItem.hoverItem = chestDisplay[obj];
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
        mouseObject.transform.SetParent(this.transform.GetChild(0));
        if (itemsDisplay.ContainsKey(obj) && itemsDisplay[obj].id >= 1)
        {
            Debug.Log("is not default item");
            var img = mouseObject.AddComponent<Image>();
            img.sprite = playerInv.database.GetItemObject[itemsDisplay[obj].id].uiDisplay;
            img.raycastTarget = false;
            mouseItem.item = itemsDisplay[obj];
            mouseItem.typeFrom = 0;
        }
        else if (chestDisplay.ContainsKey(obj) && chestDisplay[obj].id >= 1)
        {
            Debug.Log("is not default item");
            var img = mouseObject.AddComponent<Image>();
            img.sprite = chestInv.database.GetItemObject[chestDisplay[obj].id].uiDisplay;
            img.raycastTarget = false;
            mouseItem.item = chestDisplay[obj];
            mouseItem.typeFrom = 1;
        } else mouseItem.typeFrom = -1;
        mouseItem.obj = mouseObject;
    }
    public void OnDragEnd(GameObject obj)
    {
        if (mouseItem.hoverobj && mouseItem.typeFrom == 0 && mouseItem.typeTo == 0)
            playerInv.MoveItem(itemsDisplay[obj], itemsDisplay[mouseItem.hoverobj]);
        else if (mouseItem.hoverobj && mouseItem.typeFrom == 0 && mouseItem.typeTo == 1)
            playerInv.MoveItem(itemsDisplay[obj], chestDisplay[mouseItem.hoverobj]);
        else if (mouseItem.hoverobj && mouseItem.typeFrom == 1 && mouseItem.typeTo == 0)
            playerInv.MoveItem(chestDisplay[obj], itemsDisplay[mouseItem.hoverobj]);
        else if (mouseItem.hoverobj && mouseItem.typeFrom == 1 && mouseItem.typeTo == 1)
            playerInv.MoveItem(chestDisplay[obj], chestDisplay[mouseItem.hoverobj]);
        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }
    public void OnDrag(GameObject obj)
    {
        if (mouseItem.obj != null)
            mouseItem.obj.GetComponent<RectTransform>().position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -6f);
    }
}
