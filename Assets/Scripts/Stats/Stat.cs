using System;
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

[System.Serializable]
public class StatModifier : IDisposable
{
    public StatType Type { get; }
    public IOperationStrategy Strategy { get; }

    public bool MarkedForRemoval { get; private set; }
    public void MarkForRemoval()
    { MarkedForRemoval = true; }

    public event Action<StatModifier> OnDispose = delegate { };

    public StatModifier(StatType type, IOperationStrategy strategy, bool markedForRemoval = false)
    {
        Type = type;
        Strategy = strategy;
        MarkedForRemoval = markedForRemoval;
    }

    public void Dispose()
    {
        OnDispose.Invoke(this);
    }
}