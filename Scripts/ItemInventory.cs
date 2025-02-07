using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public Dictionary<string, int> ItemInv = new Dictionary<string, int>();
    public List<string> DirtyItems = new List<string>();
    public Action<ItemInventory> OnInventoryDirtied;

    public Action<ItemInventory> OnInventoryDestroyed;

    void Awake()
    {

    }

    void Update()
    {
        if (DirtyItems.Count > 0)
        {
            OnInventoryDirtied.Invoke(this);
        }
    }

    void OnDestroy()
    {
        OnInventoryDestroyed.Invoke(this);
    }
    
    public void AddItem(string itemName, int amount = 1)
    {
        if (!Items.ItemExists(itemName))
        {
            return;
        }

        if (!ItemInv.ContainsKey(itemName))
        {   // New item entry if there was none for the given item
            ItemInv.Add(itemName, amount);
            Items.ItemID[itemName].AddItemScripts(target)
            Dirty(itemName);
        }
        else
        {   // Else increment the preexisting entry for the given item
            ItemInv[itemName] += amount;
            Dirty(itemName);
        }
    }

    public void RemoveItem(string ItemName, int amount = 1)
    {
        if (!Items.ItemExists(ItemName))
        {
            return;
        }
        if (!ItemInv.ContainsKey(itemName))
        {
            return;
        }

        if (ItemInv[itemName] - amount <= 0)
        {
            ItemInv.Remove(itemName);
            Items.ItemID[itemName].RemoveItemScripts(target);
        }
        else
        {
            ItemInv[itemName] -= amount;
            Dirty(itemName);
        }
    }

    public void Dirty(string ItemName)
    {
        if (DirtyItems.Contains<string>(ItemName))
        {
            return;
        }

        DirtyItems.Add(ItemName);
    }

    public void Undirty(string ItemName)
    {
        if (!DirtyItems.Contains<string>(ItemName))
        {
            return;
        }

        DirtyItems.Remove(ItemName);
    }
}