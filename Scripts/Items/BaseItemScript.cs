using System.Linq;
using UnityEngine;

public abstract class BaseItemScript : MonoBehaviour, Items.IItem
{
    public BaseItem BaseItemSO;
    protected PlayerController controller;
    protected ItemInventory InvOwner;
    protected CharacterStats characterStats;

    public string itemName => BaseItemSO.itemName;
    public Items.ItemRarity Rarity => BaseItemSO.Rarity;
    public Items.ItemType Type => BaseItemSO.Type;
    public int Count => InvOwner.ItemInv[itemName];

    private void Awake()
    {
        InvOwner = gameObject.GetComponentInParent<ItemInventory>();
        if (!InvOwner)
        {
            Destroy(gameObject);
            return;
        }
        controller = InvOwner.GetComponent<PlayerController>();
        characterStats = InvOwner.GetComponent<CharacterStats>();
        InvOwner.Holder.ItemObjects.Add(gameObject);
    }

    private void Update()
    {

    }

    public virtual void UpdateStats()
    {
        if (!InvOwner.DirtyItems.Contains<string>(itemName))
        {
            return;
        }
        InvOwner.Undirty(itemName);
    }

    private void OnDestroy()
    {
        if (InvOwner != null)
        {
            InvOwner.ItemInv.Remove(itemName);
        }
        InvOwner.Holder.ItemObjects.Remove(gameObject);
    }
}