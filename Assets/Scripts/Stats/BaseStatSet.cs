using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseStatSet")]
public class BaseStatSet : ScriptableObject
{
    public List<Stat> StatList
    {
        get { return StatList; } private set
    {
        StatList = value;
        _statSet = new Dictionary<StatType, Stat>();
            foreach (Stat stat in StatList)
            {
                _statSet.Add(stat.Type, stat);
            }
    }   }

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
            foreach (Stat stat in StatList)
            {
                _statSet.Add(stat.Type, stat);
            }
        }
        return _statSet;
    }
}