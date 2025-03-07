using System;
using UnityEngine;

public class PingasItemScript : BaseItemScript
{
    [Header("Passive Jump Modifier")]
    public float InitialValue = 0.2f;
    public float ItemValueRate = 0.15f;
    public StatModifier Mod = StatModifier.Add;
    private float TotalValue => InitialValue + ItemValueRate * Count;

    StatMod JumpModifier;

    protected override void ItemAwake()
    {
        JumpModifier = new StatMod(StatType.JumpHeight, TotalValue, Mod); 
    }

    public override void UpdateStats()
    {
        base.UpdateStats();
        JumpModifier.Value = TotalValue;

        if (Count > 1)
        {
            characterStats.AddStatModifier(JumpModifier);
        }
    }

    protected override void ItemOnDestroy()
    {
        if (Count == 0)
        {
            characterStats.RemoveStatModifier(JumpModifier);
        }
    }
}
