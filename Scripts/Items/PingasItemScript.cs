using UnityEngine;

public class PingasItemScript : BaseItemScript
{
    StatMod JumpModifier;

    private void Start()
    {
        JumpModifier = new StatMod(StatType.JumpHeight, 2 + 1 * Count, StatModifier.Add);
    }
    public override void UpdateStats()
    {
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
