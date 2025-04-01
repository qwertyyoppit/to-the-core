public class StatModifier
{
    public StatType Type;
    public float Value;
    public IModifierOperation Operation;

    public StatModifier(StatType type, float value, IModifierOperation operation)
    {
        Type = type;
        Value = value;
        Operation = operation;
    }
}