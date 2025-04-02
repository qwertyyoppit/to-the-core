public class Stats
{
    readonly BaseStats _baseStats;
    readonly StatManager _statManager;

    public float MoveAcceleration { 
        get
        {
            return _baseStats.MoveAcceleration;
        }
    }
    public float MoveSpeed
    {
        get
        {
            return _baseStats.MoveSpeed;
        }
    }
    public float MaxHealth
    {
        get
        {
            return _baseStats.MaxHealth;
        }
    }
    public float Damage
    {
        get
        {
            return _baseStats.Damage;
        }
    }
    public float Defense
    {
        get
        {
            return _baseStats.Defense;
        }
    }

    public Stats(BaseStats baseStats)
    {
        _baseStats = baseStats;
        _statManager = new StatManager(ref baseStats);
    }
}