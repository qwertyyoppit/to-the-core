using UnityEngine;
using System.Collections.Generic;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] BaseStatSet _characterBaseStatSet;

    private Dictionary<StatType, Stat> _characterBaseStats;

    private List<StatMod> _baseStatModifiers = new List<StatMod>();

    private Dictionary<StatType, Stat> _finalStatValues = new Dictionary<StatType, Stat>();

    private bool _dirty = true;

    void Awake()
    {
        foreach(Stat stat in _characterBaseStatSet.StatList)
        {
            _characterBaseStats.Add(stat.Type, stat);
        }

        UpdateStats();
    }

    private void Update()
    {
        if (_dirty)
            UpdateStats();
    }

    private void UpdateStats()
    {
        _finalStatValues = _characterBaseStats;
        List<StatMod> modifiersToApply = _baseStatModifiers;

        foreach (StatMod mod in modifiersToApply)
        {
            if (mod.Modifier == Stat.StatModifier.Add)
            {
                _finalStatValues[mod.Type].Value += mod.Value;
                modifiersToApply.Remove(mod);
            }
        }
        foreach (StatMod mod in modifiersToApply)
        {
            if (mod.Modifier == Stat.StatModifier.Multiply)
            {
                _finalStatValues[mod.Type].Value *= mod.Value;
            }
        }
        _dirty = false;
    }

    public void AddStatModifier(StatMod mod)
    {
        _baseStatModifiers.Add(mod);
        _dirty = true;
    }

    public void RemoveStatModifier(StatMod mod)
    {
        _baseStatModifiers.Remove(mod);
        _dirty = true;
    }
}
