using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private Dictionary<string, BaseItemScript> _itemObjects = new Dictionary<string, BaseItemScript>();
    private Dictionary<string, BaseItemScript> _disabledItems = new Dictionary<string, BaseItemScript>();
    public Action<ItemInventory> OnListDirtied;

    public Action ItemInvDestroyed;

    private GameObject ItemHolder;

    private bool _hasDirty = true;
    public bool HasDirty {  get { return _hasDirty; }
        set
        {
            _hasDirty = value;
        }
    }

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
            DisableItem(itemName);
        }
        else
        {
            _itemObjects[itemName].Count -= amount;
        }
    }

    private void EnableItem(string itemName)
    {
        _itemObjects[itemName].gameObject.SetActive(true);
        _itemObjects.Add(itemName, _disabledItems[itemName]);
        _disabledItems.Remove(itemName);
    }

    public void DisableItem(string itemName)
    {
        _itemObjects[itemName].gameObject.SetActive(false);
        _disabledItems.Add(itemName, _itemObjects[itemName]);
        _itemObjects.Remove(itemName);
    }
}
