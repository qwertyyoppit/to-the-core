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
        ItemID.Add(ItemName, this);
    }
}