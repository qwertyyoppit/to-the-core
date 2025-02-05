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

    public static Action<string itemName, GameObject target> OnItemAdd;
    public static Action<string itemName, GameObject target> OnItemRemove;

    public static void AddItem(string itemName, GameObject target, int amount = 1)
    {
        if (!TryGetItem(itemName, target))
        {
            return;
        }

        Dictionary targetInv = target.GetComponent<ItemInventory>().ItemInv;

        if (!targetInv.ContainsKey(itemName))
        {
            targetInv.Add(itemName, amount);
            ItemID[itemName].AddItemScripts(target)
            targetInv.Dirty(itemName);
        }
        else
        {
            targetInv[itemName] += amount;
            targetInv.Dirty(itemName);
        }
    }

    public static void RemoveItem(string itemName, GameObject target, int amount = 1)
    {
        if (!TryGetItem(itemName, recipient))
        {
            return;
        }
    }

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