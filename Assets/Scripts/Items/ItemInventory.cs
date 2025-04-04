using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private Dictionary<ItemID, BaseItemScript> _itemObjects = new Dictionary<ItemID, BaseItemScript>();
    private Dictionary<ItemID, BaseItemScript> _disabledItems = new Dictionary<ItemID, BaseItemScript>();
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

    public void AddItem(ItemID id, int amount = 1)
    {
        if (!Items.ItemExists(id))
        {
            return;
        }

        if (!_itemObjects.ContainsKey(id))
        {
            BaseItemScript itemScript = Items.AddItemObject(id, ItemHolder);
            if (itemScript == null)
            {
                return;
            }
            _itemObjects.Add(itemScript.ID, itemScript);
            itemScript.Count = amount;
        }
        else
        {
            _itemObjects[id].Count = amount;
        }
    }

    public void RemoveItem(ItemID id, int amount = 1)
    {
        if (!Items.ItemExists(id))
        {
            return;
        }
        if (!_itemObjects.ContainsKey(id))
        {
            return;
        }

        if (_itemObjects[id].Count - amount <= 0)
        {
            DisableItem(id);
        }
        else
        {
            _itemObjects[id].Count -= amount;
        }
    }

    private void EnableItem(ItemID id)
    {
        _itemObjects[id].gameObject.SetActive(true);
        _itemObjects.Add(id, _disabledItems[id]);
        _disabledItems.Remove(id);
    }

    public void DisableItem(ItemID id)
    {
        _itemObjects[id].gameObject.SetActive(false);
        _disabledItems.Add(id, _itemObjects[id]);
        _itemObjects.Remove(id);
    }
}
