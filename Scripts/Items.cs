using System.Collections.Generic;
using UnityEngine;

public class Items
{
    public enum ItemRarity
    {
        None,
        Common,
        Rare,
        Special,
        Singular,
    }

    public enum ItemType
    {
        Passive,
        Movement,
        Active,
        Accessory,
        Special,
    }

    public static Dictionary<string, BaseItem> ItemID = new Dictionary<string, BaseItem>(); //Add from resource get type when build for now
    public interface IItem
    {
        public string itemName { get; }
        public ItemRarity Rarity { get; }
        public ItemType Type { get; }
        public int Count { get; }
    }

    public static bool ItemExists(string itemName)
    {
        if (!ItemID.ContainsKey(itemName))
        {
            Debug.LogError($"Item named {itemName} does not exist!");
            return false;
        }
        return true;
    }

    public static BaseItem GetItemSO(string itemName)
    {
        if (!ItemID.ContainsKey(itemName))
        {
            return null;
        }
        return ItemID[itemName];
    }
}