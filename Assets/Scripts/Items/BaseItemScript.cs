using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseItemScript : MonoBehaviour, IItem
{
    public BaseItemInfo ItemInfo;
    protected ItemInventory InvOwner;
    protected CharacterStats charStats;

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
                _dirty = true;
                InvOwner.HasDirty = true;
            if (!value)
                _dirty = false;
        }
    }

    protected List<StatMod> _activeModsList = new();

    protected Dictionary<StatType, StatMod[]> _cachedModsOfType = new();

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
        if (!InvOwner.TryGetComponent<CharacterStats>(out charStats))
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
        _cachedModsOfType.Clear();
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

    public StatMod[] GetActiveStatModifiers()
    {
        if (isdirty)
        {
            UpdateStats();
        }
        return _activeModsList.ToArray();
    }

    public StatMod[] GetActiveStatModifiersOfType(StatType type)
    {
        if (isdirty)
        {
            UpdateStats();
        }
        List<StatMod> typeMods = new List<StatMod>();
        foreach (StatMod mod in _activeModsList)
        {
            if (mod.Type == type)
                typeMods.Add(mod);
        }
        return typeMods.ToArray();
    }
}