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
    }

    public static BaseItemScript AddItemObject(string itemName, GameObject target)
    {
        if (!ItemExists(itemName))
        {
            return null;
        }
        BaseItemScript itemScript = GameObject.Instantiate(ItemID[itemName].ItemPrefab, target.transform).GetComponent<BaseItemScript>();

        return itemScript;
    }

    public static void RemoveItemObject(string itemName, GameObject target)
    {
        
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
}