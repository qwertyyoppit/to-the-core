using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BaseItem : ScriptableObject
{
    [Header("Item Information")]
    public string Name;
    public Items.ItemRarity Rarity;
    public ItemType Type;

    [Header("Item Effects")]
    public Component[] ItemScripts;

    public void AddItemScripts(GameObject target)
    {
        foreach(Component ItemScript in ItemScripts)
        {
            Component initializedScript = target.AddComponent(ItemScript);
            initializedScript.Base = this;
        }
    }

    public void RemoveItemScripts(GameObject target)
    {
        foreach(Component script in target.GetComponents<Component>())
        {
            if (ItemScripts.Contains(script))
            {
                Destroy(script);
            }
        }
    }
}