public class Stats
{
    readonly BaseStats _baseStats;

    public float MoveAcceleration { 
        get
        {
            return _baseStats.MoveAcceleration;
        }
    }
}