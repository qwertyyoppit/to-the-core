using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseStatSet")]
public class BaseStatSet : ScriptableObject
{
    public List<Stat> StatList;

    public Dictionary<StatType, List<Stat>> listStats;

    public Stat GetStat(StatType type)
    {
        return listStats[type];
    }

    public Dictionary<StatType, Stat> GetDictionary()
    {
        Dictionary<StatType, Stat> tempdict = new Dictionary<StatType, Stat>();
        foreach (Stat stat in StatList)
        {
            tempdict.Add(stat.Type, stat);
        }
        return tempdict;
    }
}