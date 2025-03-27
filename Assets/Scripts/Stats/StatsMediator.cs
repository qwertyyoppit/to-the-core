using System.Collections.Generic;
using System.Linq;

public class StatsMediator
{
    readonly List<StatModifier> listModifiers = new();
    readonly Dictionary<StatType, IEnumerable<StatModifier>> modifiersCache = new();
    readonly IStatModifierApplicationOrder order = new NormalStatModifierOrder();

    public void PerformQuery(object sender, Query query) {
        if (!modifiersCache.ContainsKey(query.Type)) {
            modifiersCache[query.Type] = listModifiers.Where(modifier => modifier.Type == query.Type).ToList();
        }
        query.Value = order.Apply(modifiersCache[query.Type], query.Value);
    }

    void InvalidateCache(StatType type) {
        modifiersCache.Remove(type);
    }

    public void AddModifier(StatModifier modifier) {
        listModifiers.Add(modifier);
        InvalidateCache(modifier.Type);
        modifier.MarkForRemoval();
        
        modifier.OnDispose += _ => InvalidateCache(modifier.Type);
        modifier.OnDispose += _ => listModifiers.Remove(modifier);
    }
}

public class Query
{
    public readonly StatType Type;
    public float Value;

    public Query(StatType statType, float value)
    {
        Type = statType;
        Value = value;
    }
}