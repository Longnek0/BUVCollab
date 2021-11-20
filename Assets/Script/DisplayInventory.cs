

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


//Author : Nguyen  Huu Hung Long
//Last modify date : 20/11/2021
//Display and Update inventory on screen , Handing inventory item swaping.
//Scriptable object 
//Held by Canvas/Inventory Screen


public class DisplayInventory : MonoBehaviour
{
    
    public MouseItem mouseItem = new MouseItem();// Holding item when draging within inventory

    public GameObject inventoryPrefab; // Inventory Prefab will be spawning in when play
    public InventoryObj inventory; //Inventory system storing item. InventoryObj Script
    public int X_START; //Int for X start of the canvas grid
    public int Y_START; //Int for Y start of the canvas grid
    public int X_SPACE_BETWEEN_ITEM; // Value X of how far away item is display    
    public int Y_SPACE_BETWEEN_ITEMS; //Value Y of how for away item is display
    public int NUMBER_OF_COLUMN; // Number of collum for item
    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    void Start()
    {
        CreateSlots(); // Spawn item frame(prefab) when starting
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots(); // Update data and display item every changes
    }

    //Create a slot for item to be store and display
    public void CreateSlots()
    {

        itemsDisplayed = new Dictionary<GameObject, InventorySlot>(); // giving value to itemsDisplayed 

        //Handle data within the inventoy
        for (int i = 0; i < inventory.Container.Items.Length; i++) 
        {
           
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);//Spawn inventory slot
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });//Create  Event for when mouse enter the item
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });//Create Event for when mouse exit the item
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });// Create Event for when player start drag the item
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });//Create Event for when player release the item
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });//Create Event for when player is dragging the item


            itemsDisplayed.Add(obj, inventory.Container.Items[i]); // Add item to the inventory 
        }
    }
    //Update the data of inventory
    public void UpdateSlots()
    {

        //Check if the current inventory slot if empty or not
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            //if there is an item in that slot
            if (_slot.Value.ID >= 0)
            {
                //Change the sprite of the item to the correct ID given in Database
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                //Change the color of the image back to white
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                //Update the amount of item , if item is stackable then display number , if not stackable then display nothing 
                // ("n0") is giving the "," when ever you have a long number example : 1,000 || Without it it will display only 1000
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            //Else if the current slot is empty 
            else
            {
                //Change the sprite to null
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                //Change the color back to transparant 
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                //Set the amount back to Null
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    //making a funciton for event handling
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    //On mouse enter event
    public void OnEnter(GameObject obj)
    {
        mouseItem.hoverObj = obj;
        if (itemsDisplayed.ContainsKey(obj))
            mouseItem.hoverItem = itemsDisplayed[obj];
    }
    //On mouse exit event
    public void OnExit(GameObject obj)
    {
        mouseItem.hoverObj = null; //Reset the current mouse
        mouseItem.hoverItem = null; //Reset the current mouse
    }
    //On mouse drag event
    public void OnDragStart(GameObject obj)
    {
        //Create new gameobject
        var mouseObject = new GameObject();
        //Get the transform of the new spawn item
        var rt = mouseObject.AddComponent<RectTransform>();
        //set the size of the item to be 50 in X 50 in Y
        rt.sizeDelta = new Vector2(50, 50);

        mouseObject.transform.SetParent(transform.parent);
        //If the current item is not NULL then give the new GameObject it's sprite
        if (itemsDisplayed[obj].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();//Give the new GameObject Image component
            img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].uiDisplay;//Giving the new GameObject the correct sprite
            img.raycastTarget = false; // Turn off raycast
        }
        mouseItem.obj = mouseObject;
        mouseItem.item = itemsDisplayed[obj];
    }
    //On drag end event
    public void OnDragEnd(GameObject obj)
    {
        //If you move the item to another inventory slot move the item to that slot
        if (mouseItem.hoverObj)
        {
            inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObj]);
        }
        //If you drag the item out of the inventory slot then destroy the item
        else
        {
            inventory.RemoveItem(itemsDisplayed[obj].item);
        }
        Destroy(mouseItem.obj);//Destry the current new GameObject create on the mouse
        mouseItem.item = null;// Set the item on the mouse back to NULL
    }
    //On drag event
    public void OnDrag(GameObject obj)
    {
        //If you are draging an object set the new spawn game object position to the current mouse position
        if (mouseItem.obj != null)
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    //Handing inventory slot spawning in
    public Vector3 GetPosition(int i)
    {
        //Math for spawning in inventory slot
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }
}

//New class for MouseItem
public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
}