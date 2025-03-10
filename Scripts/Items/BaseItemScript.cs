using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseItemScript : MonoBehaviour, Items.IItem
{
    public BaseItemInfo ItemInfo;
    protected PlayerController controller;
    protected ItemInventory InvOwner;
    protected CharacterStats characterStats;

    public string ItemName => ItemInfo.ItemName;
    public Items.ItemRarity Rarity => ItemInfo.Rarity;
    public Items.ItemType Type => ItemInfo.Type;
    public int Count => InvOwner.ItemInv[ItemName];

    protected List<StatMod> _activeModsList = new();

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
        _activeModsList.Clear();
        if (!InvOwner.DirtyItems.Contains<string>(ItemName))
        {
            return;
        }
        ItemUpdateStats();
        characterStats.AddStatModifierMethod(GetActiveStatModifiers());
        InvOwner.Undirty(ItemName);
    }

    protected virtual void ItemUpdateStats()
    {

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

    protected virtual StatMod[] GetActiveStatModifiers()
    {
        return _activeModsList.ToArray();
    }
}