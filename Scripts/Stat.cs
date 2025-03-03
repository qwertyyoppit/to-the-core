public struct Stat
{
    public StatType Type;
    public enum Modifiers
    {
        Base,
        Add,
        Multiply,
        Percent
    }
    public Modifiers Modifier;
    public float Value;

    public Stat(StatType type, Modifiers modifier, float value)
    {
        Type = type;
        Modifier = modifier;
        Value = value;
    }
}