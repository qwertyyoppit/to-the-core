using System.Collections.Generic;
using System.Linq;

public class StatManager
{
    readonly BaseStats baseStats;
    readonly List<StatModifier> modifiers = new();
    readonly Dictionary<StatType, IEnumerable<StatModifier>> modifiersCache = new();
    readonly IModifierApplicationOrder order = new ModifierApplicationOrder();

    public float GetStat(StatType type)
    {
        if (!modifiersCache.ContainsKey(type))
        {
            modifiersCache[type] = modifiers.Where(modifier => modifier.Type == type).ToList();
        }
        return order.Apply(modifiersCache[type], baseStats.type // use a class with type and value for basestat or something
    }

    public StatManager(ref BaseStats stats)
    {
        baseStats = stats;
    }
}