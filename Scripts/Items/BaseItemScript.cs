using System.Linq;
using UnityEngine;

public abstract class BaseItemScript : MonoBehaviour, Items.IItem
{
    public BaseItem BaseItemSO;
    protected PlayerController controller;
    protected ItemInventory InvOwner;
    protected CharacterStats characterStats;

    public string ItemName => BaseItemSO.ItemName;
    public Items.ItemRarity Rarity => BaseItemSO.Rarity;
    public Items.ItemType Type => BaseItemSO.Type;
    public int Count => InvOwner.ItemInv[ItemName];

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
        ItemAwake();
    }

    protected virtual void ItemAwake()
    {
        
    }

    private void Update()
    {

    }

    public virtual void UpdateStats()
    {
        if (!InvOwner.DirtyItems.Contains<string>(ItemName))
        {
            return;
        }
        InvOwner.Undirty(ItemName);
    }

    private void OnDestroy()
    {
        if (InvOwner != null)
        {
            InvOwner.ItemInv.Remove(ItemName);
            InvOwner.Holder.ItemObjects.Remove(gameObject);
        }
        ItemOnDestroy();
    }

    protected virtual void ItemOnDestroy()
    {

    }
}