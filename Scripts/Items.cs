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

    public static void AddItem(string itemName, GameObject target)
    {
        if (!TryGetItem(itemName, target))
        {
            return;
        }

        Dictionary targetInv = target.GetComponent<ItemInventory>().ItemInv;

        if (!targetInv.ContainsKey(itemName))
        {
            targetInv.Add(itemName, 1);
            ItemID[itemName].AddItemScripts(target)
        }
        else
        {
            targetInv[itemName] += 1;
            targetInv.DirtyItems.Add(itemName);
        }
    }

    public static void RemoveItem(string itemName, GameObject target)
    {
        if (!TryGetItem(itemName, recipient))
        {
            return;
        }

        Dictionary targetInv = recipient.GetComponent<ItemInventory>().ItemInv;

        if (!targetInv.ContainsKey(itemName))
        {
            return;
        }
        else if (targetInv[itemName] == 1)
        {
            targetInv.Remove(itemName);
            ItemID[itemName].RemoveItemScripts(target);
        }
        else
        {
            targetInv[itemName] -= 1;
            targetInv.DirtyItems.Add(itemName);
        }
    }

    public static bool TryGetItem(string itemName, GameObject? target)
    {
        if (!ItemID.ContainsKey(itemName))
        {
            Debug.LogError($"Item named {itemName} does not exist!")
            return false;
        }
        if (target == null)
        {
            Debug.LogError($"Item recipient {recipient} does not exist!");
            return false;
        }
        if (!target.GetComponent<ItemInventory>())
        {
            Debug.LogError($"Item recipient {recipient} has no ItemInventory!");
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