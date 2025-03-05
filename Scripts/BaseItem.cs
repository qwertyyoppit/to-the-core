using System.Linq;
using UnityEditor;
using UnityEngine;
using static Items;

[CreateAssetMenu(menuName ="BaseItem")]
public class BaseItem : ScriptableObject
{
    [Header("Item Information")]
    public string itemName;
    public ItemRarity Rarity;
    public ItemType Type;

    [Header("Item Effects")]
    public GameObject ItemPrefab;

    private void OnEnable()
    {
        Items.ItemID.Add(itemName, this);
    }

    public void AddItemScripts(GameObject target)
    {
        if (!target.TryGetComponent<ItemInventory>(out ItemInventory Inv))
        {
            return;
        }

        ItemHolder Holder = Inv.Holder;

        if (!Holder)
        {
            Holder = Inv.CreateItemHolder().GetComponent<ItemHolder>();
        }

        Instantiate(ItemPrefab, Holder.transform);
    }

    public void RemoveItemScripts(GameObject target)
    {
        if (!target.TryGetComponent<ItemInventory>(out ItemInventory Inv))
        {
            return;
        }

        ItemHolder Holder = Inv.Holder;

        if (!Holder)
        {
            return;
        }

        foreach(GameObject ItemScript in Holder.ItemObjects)
        {
            if (ItemScript.GetComponent<IItem>().itemName == itemName)
            {
                Holder.ItemObjects.Remove(ItemScript);
                Destroy(ItemScript);
            }
        }
    }
}