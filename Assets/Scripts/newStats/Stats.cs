using System;

public class Stats
{
    readonly BaseStats baseStats;
    readonly StatManager manager;

    public Stats(BaseStats baseStats)
    {
        this.baseStats = baseStats;
        manager = new StatManager();
    }

    public float MoveAcceleration {
        get
        {
            return manager.GetStat(StatType.MoveAcceleration, baseStats.MoveAcceleration);
        }
    }
    public float MoveSpeed
    {
        get
        {
            return manager.GetStat(StatType.MoveSpeed, baseStats.MoveSpeed);
        }
    }
    public float JumpHeight
    {
        get
        {
            return manager.GetStat(StatType.JumpHeight, baseStats.JumpHeight);
        }
    }
    public float MaxHealth
    {
        get
        {
            return manager.GetStat(StatType.MaxHealth, baseStats.MaxHealth);
        }
    }
    public float Damage
    {
        get
        {
            return manager.GetStat(StatType.Damage, baseStats.Damage);
        }
    }
    public float Defense
    {
        get
        {
            return manager.GetStat(StatType.Defense, baseStats.Defense);
        }
    }
}