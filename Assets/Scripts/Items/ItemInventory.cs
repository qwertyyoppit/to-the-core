using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private Dictionary<string, BaseItemScript> _itemObjects = new Dictionary<string, BaseItemScript>();
    private List<string> _dirtyItems = new List<string>();
    public Action<ItemInventory> OnListDirtied;

    public Action ItemInvDestroyed;

    public ItemHolder Holder;
    private GameObject ItemHolder;

    void Awake()
    {
        CreateItemHolder();
    }

    void OnDestroy()
    {
        ItemInvDestroyed.Invoke();
    }

    private void CreateItemHolder()
    {
        ItemHolder = Instantiate(new GameObject(), gameObject.transform);
        ItemHolder.name = "Items";
    }

    public void AddItem(string itemName, int amount = 1)
    {
        if (!Items.ItemExists(itemName))
        {
            return;
        }

        if (!_itemObjects.ContainsKey(itemName))
        {
            BaseItemScript itemScript = Items.AddItemObject(itemName, ItemHolder);
            if (itemScript == null)
            {
                return;
            }
            _itemObjects.Add(itemScript.ItemName, itemScript);
            itemScript.Count = amount;
            Dirty(itemName);
        }
        else
        {
            _itemObjects[itemName].Count = amount;
            Dirty(itemName);
        }
    }

    public void RemoveItem(string ItemName, int amount = 1)
    {
        if (!Items.ItemExists(ItemName))
        {
            return;
        }
        if (!_itemObjects.ContainsKey(ItemName))
        {
            return;
        }

        if (_itemObjects[ItemName].Count - amount <= 0)
        {
            _itemObjects.Remove(ItemName);
            Items.ItemID[ItemName].RemoveItemScripts(gameObject);
        }
        else
        {
            _itemObjects[ItemName].Count -= amount;
            Dirty(ItemName);
        }
    }

    public void Dirty(string ItemName)
    {
        if (_dirtyItems.Contains<string>(ItemName))
        {
            return;
        }

        _dirtyItems.Add(ItemName);
        OnListDirtied.Invoke(this);
    }

    public void Undirty(string ItemName)
    {
        if (!_dirtyItems.Contains<string>(ItemName))
        {
            return;
        }

        _dirtyItems.Remove(ItemName);
    }

    public void GetStatModifiersFromItems()
    {
        List<StatMod> mods = new List<StatMod>();
        foreach (string itemName in _itemObjects.Keys)
        {
            
        }
    }
}
