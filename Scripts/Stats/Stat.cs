using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public static List<string> Types = new();

    public enum StatModifier
    {
        Add,
        Multiply
    }

    public StatType Type;
    public float Value;
}

[System.Serializable]
public class StatMod : Stat
{
    public StatModifier Modifier;
}