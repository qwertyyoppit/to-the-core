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
        }
        else
        {
            _itemObjects[itemName].Count = amount;
        }
    }

    public void RemoveItem(string itemName, int amount = 1)
    {
        if (!Items.ItemExists(itemName))
        {
            return;
        }
        if (!_itemObjects.ContainsKey(itemName))
        {
            return;
        }

        if (_itemObjects[itemName].Count - amount <= 0)
        {
            _itemObjects.Remove(itemName);
            Items.ItemID[itemName].RemoveItemScripts(gameObject); //DEPRECATED, FIGURE OUT NEW REMOVE ITEM THING!!!!
        }
        else
        {
            _itemObjects[itemName].Count -= amount;
        }
    }

    public List<StatMod> GetStatModifiersFromItems()
    {
        List<StatMod> mods = new List<StatMod>();
        foreach (BaseItemScript itemScript in _itemObjects.Values)
        {
            mods.AddRange(itemScript.GetActiveStatModifiers());
        }
        return mods;
    }
}
