using UnityEngine;

public class PingasItemScript : BaseItemScript
{
    public override void UpdateStats()
    {
        StatMod JumpModifier = new StatMod(StatType.JumpHeight, 2 + 1 * Count, StatModifier.Add); //figure out how to make this not created each update but a not local variable

        base.UpdateStats();
        if (Count == 0)
        {
            characterStats.RemoveStatModifier(JumpModifier);
        }
        if (Count > 1)
        {
            characterStats.AddStatModifier(JumpModifier);
        }
    }
}
