using System.Collections.Generic;
using System.Linq;

public class StatManager
{
    private readonly List<StatModifier> modifiers = new();
    private readonly Dictionary<StatType, IEnumerable<StatModifier>> modifiersCache = new();
    private readonly IModifierApplicationOrder order = new ModifierApplicationOrder();

    public float GetStat(StatType type, float baseValue)
    {
        if (!modifiersCache.ContainsKey(type))
        {
            modifiersCache[type] = modifiers.Where(modifier => modifier.Type == type).ToList();
        }
        return order.Apply(modifiersCache[type], baseValue);
    }

    public void InvalidateCache(StatType type)
    {
        _ = modifiersCache.Remove(type);
    }

    public void AddModifier(StatModifier modifier)
    {
        modifiers.Add(modifier);
        InvalidateCache(modifier.Type);

        modifier.OnChanged += _ =>
        {
            InvalidateCache(modifier.Type);
        };
        modifier.OnDispose += _ =>
        {
            InvalidateCache(modifier.Type);
            _ = modifiers.Remove(modifier);
        };
    }
}