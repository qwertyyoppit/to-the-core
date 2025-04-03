using System;

public class Stats
{
    readonly BaseStats _baseStats;
    readonly StatManager _statManager;

    public float MoveAcceleration {
        get
        {
            return _statManager.GetStat(StatType.MoveAcceleration, _baseStats.MoveAcceleration);
        }
    }
    public float MoveSpeed
    {
        get
        {
            return _statManager.GetStat(StatType.MoveSpeed, _baseStats.MoveSpeed);
        }
    }
    public float MaxHealth
    {
        get
        {
            return _statManager.GetStat(StatType.MaxHealth, _baseStats.MaxHealth);
        }
    }
    public float Damage
    {
        get
        {
            return _statManager.GetStat(StatType.Damage, _baseStats.Damage);
        }
    }
    public float Defense
    {
        get
        {
            return _statManager.GetStat(StatType.Defense, _baseStats.Defense);
        }
    }

    public Stats(BaseStats baseStats)
    {
        _baseStats = baseStats;
        _statManager = new StatManager();
    }
}