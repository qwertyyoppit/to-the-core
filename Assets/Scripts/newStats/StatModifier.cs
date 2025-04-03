using System;

public class StatModifier : IDisposable
{
    public StatType Type;
    public float Value;
    public IModifierOperation Operation;

    public bool MarkedForDisposal { get; private set; } = false;
    public void MarkForDisposal()
    {  
        MarkedForDisposal = true;
    }

    public Action<object> OnDispose = delegate { };

    public StatModifier(StatType type, float value, OperationOrder order)
    {
        Type = type;
        Value = value;
        switch (order)
        {
            case OperationOrder.AddOperation:
                Operation = new AddOperation(ref value); break;
            case OperationOrder.MultiplyOperation:
                Operation = new MultiplyOperation(ref value); break;
        }
    }
    public float Calculate(float value) => Operation.Calculate(value);

    public void Dispose()
    {
        OnDispose.Invoke(this);
    }
}