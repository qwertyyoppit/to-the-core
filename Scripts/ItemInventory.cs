using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public Dictionary<string, int> ItemInv = new Dictionary<string, int>();
    public List<string> DirtyItems = new List<string>();

    public Action<ItemInventory> OnItemInvDestroyed;

    void Awake()
    {
        Items.OnItemAdd += AddItemScripts;
        Items.OnItemRemove += RemoveItemScripts;
    }

    void Update()
    {
        if (DirtyItems.Count > 0)
        {
            
        }
    }

    void OnDestroy()
    {
        OnItemInvDestroyed.Invoke(this);
    }
}