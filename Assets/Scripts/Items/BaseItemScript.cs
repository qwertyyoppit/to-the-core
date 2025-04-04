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

    [SerializeField] protected bool _dirty = true;

    public bool isdirty { get
        {
            return _dirty;
        }
        protected set
        {
            if (value)
            {
                _dirty = true;
            }
            if (!value)
            {
                _dirty = false;
            }
        }
    }

    protected List<StatModifier> _activeModsList = new();

    private void Awake()
    {
        gameObject.name = ItemName;
        ItemAwake();
    }

    protected virtual void ItemAwake() { }

    private void Start()
    {
        if (!transform.parent.TryGetComponent<ItemInventory>(out InvOwner))
        {
            Destroy(gameObject);
            return;
        }
        if (!InvOwner.TryGetComponent<Entity>(out _)) //Entity l9519159919548956374
        {
            Destroy(gameObject);
            return;
        }
        ItemStart();
    }

    protected virtual void ItemStart() { }

    private void Update()
    {
        if (Count != _count) //Dirty this if count changed/different than before
        {
            _count = Count;
            isdirty = true;
        }
        if (isdirty)
        {
            HandleStats();
        }
        ItemUpdate();
    }

    protected virtual void ItemUpdate() { }

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
        isdirty = false;
    }

    protected virtual void ItemHandleStats() { }

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

    protected virtual void ItemOnEnable() { }

    private void OnDisable()
    {
        Count = 0;
        _activeModsList.Clear();
        ItemOnDisable();
    }

    protected virtual void ItemOnDisable() { }
}