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
            var initializedScript = (ItemScript.GetClass())target.AddComponent(ItemScript.GetClass());
            initializedScript.BaseItemSO = this;
        }
    }

    public void RemoveItemScripts(GameObject target)
    {
        foreach (MonoBehaviour script in target.GetComponents<MonoBehaviour>())
        {
            foreach (ItemScript in ItemScripts)
            {
                if (script.IsAssignableFrom(ItemScript))
                {
                    Destroy(script);
                }
            }
        }
    }
}