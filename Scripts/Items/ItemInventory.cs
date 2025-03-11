using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public Dictionary<string, int> ItemInv = new Dictionary<string, int>();
    private Dictionary<string, BaseItemScript> _itemObjects = new Dictionary<string, BaseItemScript>();
    private List<string> DirtyItems = new List<string>();
    public Action<ItemInventory> OnListDirtied;

    public Action ItemInvDestroyed;

    public ItemHolder Holder;
    private GameObject ItemHolder;

    void Awake()
    {
        ItemHolder = Instantiate(new GameObject(), gameObject.transform);
        CreateItemHolder();
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
        ItemInvDestroyed.Invoke();
    }

    public GameObject CreateItemHolder()
    {
        GameObject newHolder = Instantiate(new GameObject(), gameObject.transform);
        newHolder.AddComponent<ItemHolder>();
        return newHolder;
    }

    public void AddItem(string ItemName, int amount = 1)
    {
        if (!Items.ItemExists(ItemName))
        {
            return;
        }

        if (!ItemInv.ContainsKey(ItemName))
        {
            ItemInv.Add(ItemName, amount);
            BaseItemScript newscript = Items.ItemID[ItemName].AddItemScripts(gameObject);
            //newscript.setcount(amount);
            Dirty(ItemName);
        }
        else
        {
            ItemInv[ItemName] += amount;
            Dirty(ItemName);
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

    public void GetStatModifiersFromItems()
    {
        List<StatMod> mods = new List<StatMod>();
        foreach (string ItemName in ItemInv.Keys)
        {
            
        }
    }
}
