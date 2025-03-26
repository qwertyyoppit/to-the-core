public class Stats
{
    readonly StatsMediator mediator;
    readonly BaseStatSet baseStats;

    public StatsMediator Mediator => mediator;

    public float GetStat(StatType type)
    {
        var q = new Query(type, baseStats.GetStat(type).Value)
        mediator.PerformQuery(this, q);
        return q.Value;
    }
    
    public Stats(StatsMediator mediator, BaseStatSet baseStats) {
        this.mediator = mediator;
        this.baseStats = baseStats;
    }
}