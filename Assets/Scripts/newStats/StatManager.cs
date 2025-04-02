using System.Collections.Generic;
using System.Linq;

public class StatManager
{
    readonly BaseStats baseStats;
    readonly List<StatModifier> modifiers = new();
    readonly Dictionary<StatType, IEnumerable<StatModifier>> modifiersCache = new();
    readonly IModifierApplicationOrder order = new ModifierApplicationOrder();

    public float GetStat(StatType type, float baseValue)
    {
        if (!modifiersCache.ContainsKey(type))
        {
            modifiersCache[type] = modifiers.Where(modifier => modifier.Type == type).ToList();
        }
        return order.Apply(modifiersCache[type], baseValue)
    }

    public StatModifier AddModifier()

    public StatManager(ref BaseStats stats)
    {
        baseStats = stats;
    }
}