using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] BaseStatSet _characterBaseStatSet;

    private Dictionary<StatType, Stat> _characterBaseStats;

    private List<StatMod> _baseStatModifiers = new List<StatMod>();

    private Dictionary<StatType, Stat> _characterStats = new Dictionary<StatType, Stat>();

    private bool _dirty = true;

    void Awake()
    {
        _characterBaseStats = Instantiate(_characterBaseStatSet).GetDictionary(); //todo: save new stat types instead of references so the SO isnt being modified (float instead of stat value?)

        UpdateStats();
    }

    public float GetStat(StatType type)
    {
        if (_dirty)
            UpdateStats();
        Debug.Log("aaaa");
        return _characterStats[type].Value;
    }

    private void UpdateStats()
    {
        _characterStats.Clear();
        foreach (KeyValuePair<StatType, Stat> stat in _characterBaseStats)
        {
            _characterStats.Add(stat.Key, new Stat(stat.Value.Type, stat.Value.Value)); //UNTESTED TODO: TEST IT!!!!!!!!!!
        }
        List<StatMod> modifiersToApply = new (_baseStatModifiers);

        foreach (StatMod mod in modifiersToApply)
        {
            if (mod.Modifier == StatModifier.Add)
            {
                _characterStats[mod.Type].Value += mod.Value;
            }
        }
        foreach (StatMod mod in modifiersToApply)
        {
            if (mod.Modifier == StatModifier.Multiply)
            {
                _characterStats[mod.Type].Value *= mod.Value;
            }
        }
        _dirty = false;
    }

    public void AddStatModifier(StatMod mod)
    {
        if (_baseStatModifiers.Contains(mod))
        {
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
