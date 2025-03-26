using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseStatSet")]
public class BaseStatSet : ScriptableObject
{
    [SerializeField] private List<Stat> _statList { get; private set
    {
        _statList = value;
        _statSet = new Dictionary<StatType, Stat>();
            foreach (Stat stat in _statList)
            {
                _statSet.Add(stat.Type, stat);
            }
    }};

    private Dictionary<StatType, Stat> _statSet;

    public Stat GetStat(StatType type)
    {
        return GetStatSet()[type];
    }

    public Dictionary<StatType, Stat> GetStatSet()
    {
        if (_statSet == null) 
        {
            _statSet = new Dictionary<StatType, Stat>();
            foreach (Stat stat in _statList)
            {
                _statSet.Add(stat.Type, stat);
            }
        }
        return _statSet;
    }
}