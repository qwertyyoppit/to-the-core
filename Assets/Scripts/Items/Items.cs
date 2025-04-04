using System.Collections.Generic;
using UnityEngine;
using static Items;

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

    public static Dictionary<ItemID, BaseItemInfo> ItemList = new Dictionary<ItemID, BaseItemInfo>(); //Add from resource get type when build for now

    public static BaseItemScript AddItemObject(ItemID id, GameObject target)
    {
        if (!ItemExists(id))
        {
            return null;
        }
        BaseItemScript itemScript = GameObject.Instantiate(ItemList[id].ItemPrefab, target.transform).GetComponent<BaseItemScript>();

        return itemScript;
    }

    public static void RemoveItemObject(string itemName, GameObject target)
    {
        
    }

    public static bool ItemExists(ItemID id)
    {
        if (!ItemList.ContainsKey(id))
        {
            Debug.LogError($"Item named {id} does not exist!");
            return false;
        }
        return true;
    }
}
public interface IItem
{
    public ItemID ID { get; }
    public string ItemName { get; }
    public ItemRarity Rarity { get; }
    public ItemType Type { get; }

    public virtual void ItemStart() { }

    public virtual void ItemUpdate() { }

    public virtual void ItemRemove() { }
}