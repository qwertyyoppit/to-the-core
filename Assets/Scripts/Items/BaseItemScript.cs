using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemScript : MonoBehaviour, IItem
{
    public BaseItemInfo ItemInfo;
    protected ItemInventory InvOwner;
    protected Stats stats;

    public ItemID ID => ItemInfo.ID;
    public string ItemName => ItemInfo.ItemName;
    public Items.ItemRarity Rarity => ItemInfo.Rarity;
    public Items.ItemType Type => ItemInfo.Type;

    protected int _count = 0;
    public int Count = 0;

    protected List<StatModifier> _activeModsList = new();

    private void Awake()
    {
        gameObject.name = ItemName;
        ItemAwake();
    }

    private void Start()
    {
        if (!transform.TryGetComponent(out InvOwner))
        {
            Destroy(gameObject);
            return;
        }
        if (!InvOwner.TryGetComponent<Entity>(out Entity entity)) //Entity l9519159919548956374
        {
            Destroy(gameObject);
            return;
        }
        stats = entity.Stats;
        ItemStart();
    }

    private void Update()
    {
        if (Count != _count) //Dirty this if count changed/different than before
        {
            _count = Count;
            HandleStats();
        }
        ItemUpdate();
    }

    public virtual void HandleStats()
    {
        _activeModsList.Clear();
        if (Count <= 0)
        {
            Count = 0;
            ItemRemove();
        }
        else
        {
            ItemHandleStats();
        }
    }

    public virtual void ItemRemove()
    {
        InvOwner.DisableItem(ID);
    }

    private void OnEnable()
    {
        Count = 0;
        _activeModsList.Clear();
        ItemOnEnable();
    }

    private void OnDisable()
    {
        Count = 0;
        _activeModsList.Clear();
        ItemOnDisable();
    }

    protected virtual void ItemAwake() { }

    protected virtual void ItemStart() { }

    protected virtual void ItemUpdate() { }

    protected virtual void ItemHandleStats() { }

    protected virtual void ItemOnEnable() { }

    protected virtual void ItemOnDisable() { }
}