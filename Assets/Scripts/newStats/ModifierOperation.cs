public enum OperationOrder
{
    AddOperation,
    MultiplyOperation,
}
public interface IModifierOperation
{
    public float Calculate(float value);
}

public class AddOperation : IModifierOperation
{
    readonly float _value;
    public AddOperation(ref float value)
    {
        _value = value;
    }

    public float Calculate(float value) => value + _value;
}

public class MultiplyOperation : IModifierOperation
{
    readonly float _value;
    public MultiplyOperation(ref float value)
    {
        _value = value;
    }

    public float Calculate(float value) => value * _value;
}