using System.Linq;
using UnityEditor;
using UnityEngine;
using static Items;

[CreateAssetMenu(menuName ="BaseItemInfo")]
public class BaseItemInfo : ScriptableObject
{
    [Header("Item Information")]
    public ItemID ID;
    public string ItemName;
    public ItemRarity Rarity;
    public ItemType Type;

    [Header("Item Effects")]
    public GameObject ItemPrefab;

    private void OnEnable()
    {
        _ = ItemList.TryAdd(ID, this);
    }
}