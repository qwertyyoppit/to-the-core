using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseItemScript : MonoBehaviour, Items.IItem
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

    public bool isdirty => _dirty;

    protected List<StatMod> _activeModsList = new();

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
        InvOwner = gameObject.GetComponentInParent<ItemInventory>();
        if (InvOwner is null)
        {
            Destroy(gameObject);
            return;
        }
        charStats = InvOwner.GetComponent<CharacterStats>();
        if (charStats is null)
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
        if (Count <= 0)
        {
            Count = 0;
            gameObject.SetActive(false);
        }
        _activeModsList.Clear();
        ItemUpdateStats();
        _dirty = false;
    }

    protected virtual void ItemUpdateStats()
    {

    }

    private void OnDisable()
    {
        _activeModsList = null;
        ItemOnDisable();
    }

    protected virtual void ItemOnDisable()
    {

    }

    public StatMod[] GetActiveStatModifiers()
    {
        if (_dirty)
        {
            UpdateStats();
        }
        return _activeModsList.ToArray();
    }
}