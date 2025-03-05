using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public StatType Type;
    public float Value;
}

[System.Serializable]
public class StatMod : Stat
{
    public StatModifier Modifier;
}