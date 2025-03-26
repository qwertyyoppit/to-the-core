

public class StatsMediator
{
    readonly List<StatModifier> listModifiers = new();
    readonly Dictionary<StatType, IEnumerable<StatModifier>> modifiersCache = new();

    public void PerformQuery(object sender, Query query) {
        if (!modifiersCache.ContainsKey(query.Type)) {
            modifiersCache[query.StatType] = listModifiers.Where(modifier => modifier.Type == query.StatType).ToList();
        }
        query.Value = order.Apply(modifiersCache[query.Type], query.Value);
    }

    void InvalidateCache(StatType type) {
        modifiersCache.Remove(type);
    }

    public void AddModifier(StatModifier modifier) {
        listModifiers.Add(modifier);
        InvalidateCache(modifier.Type);
        modifier.MarkedForRemoval = false;
        
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