using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public StatType Type;
    public float Value;

    public Stat(StatType type, float value)
    {
        Type = type;
        Value = value;
    }
}

[System.Serializable]
public class StatMod : Stat
{
    public StatModType Modifier;

    public StatMod(StatType type, float value, StatModType mod) : base(type, value)
    {
        Modifier = mod;
    }
}