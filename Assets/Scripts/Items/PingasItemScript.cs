using System;
using UnityEngine;

public class PingasItemScript : BaseItemScript
{
    [Header("Passive Jump Modifier")]
    [SerializeField] ItemEffectStat JumpEffect;
    private float TotalValue => JumpEffect.InitialValue + JumpEffect.StackedValue * _count;

    StatMod JumpModifier;

    protected override void ItemAwake()
    {
        JumpModifier = new StatMod(StatType.JumpHeight, TotalValue, JumpEffect.ModType); 
    }

    protected override void ItemUpdateStats()
    {
        JumpModifier.Value = TotalValue;

        if (_count > 1)
        {
            _activeModsList.Add(JumpModifier);
        }
    }

    protected override void ItemOnDestroy()
    {
        base.ItemOnDestroy();
    }
}
