using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] BaseStatSet _characterBaseStatSet;

    private ItemInventory _itemInv;

    private Dictionary<StatType, Stat> _characterBaseStats;

    private readonly List<StatMod> _statModifiers = new List<StatMod>();

    private Dictionary<StatType, Stat> _characterStats = new Dictionary<StatType, Stat>();

    private List<StatType> _dirtyStats = new List<StatType>();

    void Awake()
    {
        _characterBaseStats = Instantiate(_characterBaseStatSet).GetDictionary();

        UpdateStats();
    }

    private void Start()
    {
        _itemInv = GetComponent<ItemInventory>();
    }

    public float GetStat(StatType type)
    {
        if (_dirtyStats.Contains(type))
            UpdateStat(type);
        Debug.Log(_characterStats[type].Value);
        return _characterStats[type].Value;
    }

    private void UpdateStat(StatType type)
    {
        List<StatMod> typeMod = new();
        typeMod = GetStatModifiersOfType(type);

        _characterStats[type].Value = _characterBaseStats[type].Value;
        foreach (StatMod mod in typeMod)
        {
            if (mod.Modifier == StatModType.Add)
            {
                _characterStats[type].Value += mod.Value;
            }
        }
        foreach (StatMod mod in typeMod)
        {
            if (mod.Modifier == StatModType.Multiply)
            {
                _characterStats[type].Value *= mod.Value;
            }
        }
        _dirtyStats.Remove(type);
    }

    private void UpdateStats()
    {
        UpdateStatModifiers();
        _characterStats.Clear();
        foreach (KeyValuePair<StatType, Stat> stat in _characterBaseStats)
        {
            _characterStats.Add(stat.Key, new Stat(stat.Value.Type, stat.Value.Value));
        }
        List<StatMod> modifiersToApply = new (_statModifiers);

        foreach (StatMod mod in modifiersToApply)
        {
            if (mod.Modifier == StatModType.Add)
            {
                _characterStats[mod.Type].Value += mod.Value;
            }
        }
        foreach (StatMod mod in modifiersToApply)
        {
            if (mod.Modifier == StatModType.Multiply)
            {
                _characterStats[mod.Type].Value *= mod.Value;
            }
        }
    }

    private void UpdateStatModifiers()
    {
        _statModifiers.Clear();
        _statModifiers.AddRange(_itemInv.GetStatModifiersFromItems());
    }

    private List<StatMod> GetStatModifiersOfType(StatType type)
    {
        List<StatMod> mods = new List<StatMod>();
        foreach (StatMod mod in _statModifiers)
        {
            if (mod.Type == type)
            {
                mods.Add(mod);
            }
        }
        return mods;
    }

    private List<StatMod> GetStatModifiersFromInv(ItemInventory itemInv)
    {
        List<StatMod> mods = new List<StatMod>();
        foreach (StatMod mod in _statModifiers)
        {
            if (mod.Type == type)
            {
                mods.Add(mod);
            }
        }
        return mods;
    }

    public void AddStatModifier(StatMod mod)
    {
        if (_statModifiers.Contains(mod))
        {
            _statModifiers.Find(match => match.Equals(mod)).Value = mod.Value;
            return;
        }
        _statModifiers.Add(mod);
    }

    public void RemoveStatModifier(StatMod mod)
    {
        if (!_statModifiers.Contains(mod))
        {
            return;
        }
        _statModifiers.Remove(mod);
    }
}
