using System.Collections.Generic;
using UnityEngine;

public class Items
{
    public static enum ItemRarity
    {
        None,
        Common,
        Rare,
        Special,
        Singular,
    }

    public static enum ItemType
    {
        Passive,
        Movement,
        Active,
        Accessory,
        Special,
    }

    public static Dictionary<string, ScriptableObject> ItemID = new Dictionary<string ScriptableObject>(); //Add from resource get type when build for now

    public static bool ItemExists(string itemName)
    {
        if (!ItemID.ContainsKey(itemName))
        {
            Debug.LogError($"Item named {itemName} does not exist!")
            return false;
        }
        return true;
    }

    public static ScriptableObject GetItemSO(string itemName)
    {
        if (ItemID.ContainsKey(itemName))
        {
            return ItemID[itemName];
        }
    }
}