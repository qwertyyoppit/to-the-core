using System;
using UnityEngine;

public class PingasItemScript : BaseItemScript
{
    [SerializeField] private ItemEffectStat JumpEffect;
    private float TotalValue => JumpEffect.InitialValue + (JumpEffect.StackedValue * _count);

    private StatModifier JumpModifier;

    protected override void ItemAwake()
    {
        JumpModifier = new StatModifier(StatType.JumpHeight, TotalValue, JumpEffect.ModType);
    }

    protected override void ItemHandleStats()
    {
        JumpModifier.Value = TotalValue;
    }

    protected override void ItemOnEnable()
    {
        stats.Manager.AddModifier(JumpModifier);
    }

    protected override void ItemOnDisable()
    {
        JumpModifier.MarkForDisposal();
    }
}
