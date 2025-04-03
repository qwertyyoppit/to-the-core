using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemScript : MonoBehaviour, IItem
{
    public BaseItemInfo ItemInfo;
    protected ItemInventory InvOwner;
    protected Stats stats;

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
                InvOwner.HasDirty = true;
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

    protected virtual void ItemAwake()
    {

    }

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

    protected virtual void ItemStart()
    {

    }

    private void Update()
    {
        if (Count != _count) //Dirty this if count changed/different than before
        {
            _count = Count;
            isdirty = true;
        }
        if (isdirty)
        {
            UpdateStats();
        }
        ItemUpdate();
    }

    protected virtual void ItemUpdate()
    {

    }

    public virtual void UpdateStats()
    {
        _activeModsList.Clear();
        if (Count <= 0)
        {
            Count = 0;
            ItemRemove();
        }
        ItemUpdateStats();
        isdirty = false;
    }

    protected virtual void ItemUpdateStats()
    {

    }

    public virtual void ItemRemove()
    {
        InvOwner.DisableItem(ItemName);
    }

    private void OnDisable()
    {
        Count = 0;
        _activeModsList = null;
        ItemOnDisable();
    }

    protected virtual void ItemOnDisable()
    {

    }
}