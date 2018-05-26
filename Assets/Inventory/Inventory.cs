using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private Dictionary<ItemType, Item> items;
    private GameObject inventoryPanel;
    public int money;
    private InventorySlot[] slots;


    public Item Put(Item item)
    {
        Item drop = null;
        if(items[item.type] == null)
        {
            items[item.type] = item;
        }
        else
        {
            Item good;
            Item bad;
           
            if (items[item.type].CompareTo(item) == -1)
            {
                bad = items[item.type];
                good = item; 
            }
            else
            {
                bad = item;
                good = items[item.type];
            }

            items[item.type] = good;
            drop = bad;
        }

        return drop;
    }

    public Item Drop(ItemType t)
    {
        Item drop = items[t];
        items[t] = null;
        return drop;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
            switchVisible();

    }

    private void switchVisible()
    {
        if (inventoryPanel.activeSelf)
            inventoryPanel.SetActive(false);
        else
        {
            inventoryPanel.SetActive(true);
            int i = 0;
            foreach (var val in Enum.GetValues(typeof(ItemType)))
            {
                ItemType key = (ItemType)val;
                var item = items.ContainsKey(key) ? items[key] : null;
                slots[i++].SetItem(item);
            }
        }
    }
    
    public Item Peek(ItemType t)
    {
        return items[t];
    }

    public void Start()
    {
        items = new Dictionary<ItemType, Item>();
        slots = new InventorySlot[Enum.GetNames(typeof(ItemType)).Length];
        inventoryPanel = GameObject.FindGameObjectWithTag("Inventory");
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);

        foreach (var t in Enum.GetValues(typeof(ItemType)))
        {
            items.Add((ItemType)t, null);
        }

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = inventoryPanel.transform.GetChild(i).gameObject.GetComponent<InventorySlot>();
        }
    }

    

    
}
