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

    public void RemoveItemScripts(GameObject target)
    {
        if (!target.TryGetComponent<ItemInventory>(out ItemInventory Inv))
        {
            return;
        }

        

        //if (!Holder)
        //{
        //    return;
        //}

        //foreach(GameObject ItemScript in Holder.ItemObjects)
        //{
        //    if (ItemScript.GetComponent<IItem>().ItemName == ItemName)
        //    {
        //        Destroy(ItemScript);
        //        return;
        //    }
        //}
    }
}