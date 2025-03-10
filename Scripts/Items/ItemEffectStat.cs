using UnityEngine;

[System.Serializable]
public class ItemEffectStat
{
    public StatType Type;

    public float InitialValue;
    public float StackedValue;
    public bool IncludeStackedInInitial = false;
    public StatModType ModType = StatModType.Add;
}
