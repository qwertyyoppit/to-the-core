using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseItemScript : MonoBehaviour, Items.IItem
{
    public BaseItemInfo ItemInfo;
    protected PlayerController controller;
    protected ItemInventory InvOwner;
    protected CharacterStats characterStats;

    [SerializeField] protected int _count = 0;
    public int Count { get
        {
            return _count;
        }
        set
        {
            _count = value;
        }
    }
    public string ItemName => ItemInfo.ItemName;
    public Items.ItemRarity Rarity => ItemInfo.Rarity;
    public Items.ItemType Type => ItemInfo.Type;

    protected List<StatMod> _activeModsList = new();

    private void Awake()
    {
        InvOwner = gameObject.GetComponentInParent<ItemInventory>();
        if (InvOwner is null)
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
        ItemUpdateStats();
        InvOwner.Undirty(ItemName);
    }

    protected virtual void ItemUpdateStats()
    {

    }

    private void OnDestroy()
    {
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