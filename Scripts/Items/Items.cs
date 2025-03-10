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

    public static Dictionary<string, BaseItemInfo> ItemID = new Dictionary<string, BaseItemInfo>(); //Add from resource get type when build for now
    public interface IItem
    {
        public string ItemName { get; }
        public ItemRarity Rarity { get; }
        public ItemType Type { get; }
        public int Count { get; }
    }

    public static bool ItemExists(string ItemName)
    {
        if (!ItemID.ContainsKey(ItemName))
        {
            Debug.LogError($"Item named {ItemName} does not exist!");
            return false;
        }
        return true;
    }

    public static BaseItemInfo GetItemSO(string ItemName)
    {
        if (!ItemID.ContainsKey(ItemName))
        {
            return null;
        }
        return ItemID[ItemName];
    }
}