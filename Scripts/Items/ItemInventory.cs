using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public Dictionary<string, int> ItemInv = new Dictionary<string, int>();
    public List<string> DirtyItems = new List<string>();
    public Action<ItemInventory> OnListDirtied;

    public Action<ItemInventory> ItemInvDestroyed;

    public ItemHolder Holder;

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
        if (this)
            ItemInvDestroyed.Invoke(this);
    }

    public GameObject CreateItemHolder()
    {
        GameObject newHolder = Instantiate(new GameObject(), gameObject.transform);
        newHolder.AddComponent<ItemHolder>();
        return newHolder;
    }

    public void AddItem(string itemName, int amount = 1)
    {
        if (!Items.ItemExists(itemName))
        {
            return;
        }

        if (!ItemInv.ContainsKey(itemName))
        {
            ItemInv.Add(itemName, amount);
            Items.ItemID[itemName].AddItemScripts(this.gameObject);
            Dirty(itemName);
        }
        else
        {
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
        if (!ItemInv.ContainsKey(ItemName))
        {
            return;
        }

        if (ItemInv[ItemName] - amount <= 0)
        {
            ItemInv.Remove(ItemName);
            Items.ItemID[ItemName].RemoveItemScripts(this.gameObject);
        }
        else
        {
            ItemInv[ItemName] -= amount;
            Dirty(ItemName);
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
