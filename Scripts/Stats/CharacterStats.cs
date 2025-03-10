using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] BaseStatSet _characterBaseStatSet;

    private Dictionary<StatType, Stat> _characterBaseStats;

    private List<StatMod[]> _getStatModMethods = new();

    private readonly List<StatMod> _baseStatModifiers = new List<StatMod>();

    private Dictionary<StatType, Stat> _characterStats = new Dictionary<StatType, Stat>();

    private bool _dirty = true;

    void Awake()
    {
        _characterBaseStats = Instantiate(_characterBaseStatSet).GetDictionary();

        UpdateStats();
    }

    public float GetStat(StatType type)
    {
        if (_dirty)
            UpdateStats();
        Debug.Log(_characterStats[type].Value);
        return _characterStats[type].Value;
    }

    private void UpdateStats()
    {
        _baseStatModifiers.Clear();
        foreach (StatMod[] mod in _getStatModMethods)
        {
            if (mod == null)
            {
                _getStatModMethods.Remove(mod);
                continue;
            }

            _baseStatModifiers.AddRange(mod);
        }
        _characterStats.Clear();
        foreach (KeyValuePair<StatType, Stat> stat in _characterBaseStats)
        {
            _characterStats.Add(stat.Key, new Stat(stat.Value.Type, stat.Value.Value));
        }
        List<StatMod> modifiersToApply = new (_baseStatModifiers);

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
        _dirty = false;
    }

    public void AddStatModifierMethod(StatMod[] mod)
    {
        if (mod == null)
        {
            return;
        }

        _getStatModMethods.Add(mod);
    }

    public void AddStatModifier(StatMod mod)
    {
        if (_baseStatModifiers.Contains(mod))
        {
            _baseStatModifiers.Find(match => match.Equals(mod)).Value = mod.Value;
            _dirty = true;
            return;
        }
        _baseStatModifiers.Add(mod);
        _dirty = true;
    }

    public void RemoveStatModifier(StatMod mod)
    {
        if (!_baseStatModifiers.Contains(mod))
        {
            return;
        }
        _baseStatModifiers.Remove(mod);
        _dirty = true;
    }
}
