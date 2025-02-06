using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BaseItem : ScriptableObject
{
    [Header("Item Information")]
    public string Name;
    public Items.ItemRarity Rarity;
    public Items.ItemType Type;

    [Header("Item Effects")]
    public MonoScript[] ItemScripts; //TODO: figure out what should be a MonoScript or a Component or MonoBehaviour...

    public void AddItemScripts(GameObject target)
    {
        foreach(MonoScript ItemScript in ItemScripts)
        {
            Component initializedScript = target.AddComponent(ItemScript.GetClass());
            initializedScript.BaseItemSO = this;
        }
    }

    public void RemoveItemScripts(GameObject target)
    {
        foreach(Component script in target.GetComponents<MonoBehaviour>())
        {
            if (ItemScripts.Contains(script))
            {
                Destroy(script);
            }
        }
    }
}