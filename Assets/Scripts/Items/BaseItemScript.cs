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


    protected int _count = 0;
    public int Count = 0;

    [SerializeField] protected bool _dirty = true;

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
        
        ItemAwake();
    }

    protected virtual void ItemAwake()
    {

    }

    private void Update()
    {
        if (Count != _count)
        {
            _count = Count;
            _dirty = true;
        }
        if (_dirty)
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
        ItemUpdateStats();
        _dirty = false;
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
        if (_dirty)
        {
            UpdateStats();
        }
        return _activeModsList.ToArray();
    }
}