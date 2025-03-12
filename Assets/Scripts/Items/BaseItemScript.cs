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

    protected List<StatMod> _activeModsList = new();

    private void Awake()
    {
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

    protected virtual void ItemOnDestroy() //Change to OnDisable() for unsubscribing if called upon destruction as well 
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