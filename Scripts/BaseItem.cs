using System;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class BaseItem : ScriptableObject
{
    [Header("Item Information")]
    public string Name;
    public ItemRarity Rarity;
    public ItemType Type;

    [Header("Item Effects")]
    public MonoScript[] ItemScripts;

    public void AddItemScripts(GameObject target)
    {
        foreach(MonoScript ItemScript in ItemScripts)
        {
            MonoScript initializedScript = target.AddComponent(ItemScript);
            initializedScript.Base = this;
        }
    }

    public void RemoveItemScripts(GameObject target)
    {
        foreach(MonoScript script in target.GetComponents<MonoScript>())
        {
            if (ItemScripts.Contains(script))
            {
                Destroy(script);
            }
        }
    }
}