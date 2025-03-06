using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseStatSet")]
public class BaseStatSet : ScriptableObject
{
    [SerializeField] private List<Stat> StatList;

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