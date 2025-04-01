public enum OperationOrder
{
    AddOperation,
    MultiplyOperation,
}
public interface IModifierOperation
{
    readonly OperationOrder;
    readonly ref float _value;
    public float Calculate(float value);
}

public class AddOperation
{
    OperationOrder = OperationOrder.AddOperation;
    
}