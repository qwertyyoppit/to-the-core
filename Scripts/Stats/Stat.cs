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
    public StatModifier Modifier;

    public StatMod(StatType type, float value, StatModifier mod) : base(type, value)
    {
        Modifier = mod;
    }
}