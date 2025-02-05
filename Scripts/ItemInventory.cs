using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public Dictionary<string, int> ItemInv = new Dictionary<string, int>();
    public List<string> DirtyItems = new List<string>();
    public Action<ItemInventory> OnListDirtied;

    public Action<ItemInventory> OnItemInvDestroyed;

    void Awake()
    {

    }

    void Update()
    {
        if (DirtyItems.Count > 0)
        {
            OnListDirtied.Invoke(this);
        }
    }

    void OnDestroy()
    {
        OnItemInvDestroyed.Invoke(this);
    }

    public void RemoveItem(string ItemName)
    {
        if (!Items.ItemExists(ItemName))
        {
            return;
        }
        if (!ItemInv.ContainsKey(itemName))
        {
            return;
        }

        if (ItemInv[itemName] - amount >= 0)
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