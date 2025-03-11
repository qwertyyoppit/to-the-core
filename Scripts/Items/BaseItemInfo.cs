using System.Linq;
using UnityEditor;
using UnityEngine;
using static Items;

[CreateAssetMenu(menuName ="BaseItemInfo")]
public class BaseItemInfo : ScriptableObject
{
    [Header("Item Information")]
    public string ItemName;
    public ItemRarity Rarity;
    public ItemType Type;

    [Header("Item Effects")]
    public GameObject ItemPrefab;

    private void OnEnable()
    {
        Items.ItemID.Add(ItemName, this);
    }

    public BaseItemScript AddItemScripts(GameObject target) /*TODO: Move to Items to have an ItemName parameter. would be beter i tink
    Actually maybe you should just merge this with an AddItem() thingamabob following the ItemInv-ItemHolder merge
    And make a better return item script thing. please! and thank! you :index_finger_pointing_at_viewer:*/
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

        BaseItemScript script = Instantiate(ItemPrefab, Holder.transform).GetComponent<BaseItemScript>();
        return script;
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
            if (ItemScript.GetComponent<IItem>().ItemName == ItemName)
            {
                Destroy(ItemScript);
                return;
            }
        }
    }
}