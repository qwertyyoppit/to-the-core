using System.Collections.Generic;
using System.Linq;

public interface IModifierApplicationOrder
{
    public float Apply(IEnumerable<StatModifier> mods, float baseValue);
}

public class ModifierApplicationOrder : IModifierApplicationOrder
{
    public float Apply(IEnumerable<StatModifier> mods, float baseValue)
    {
        foreach (StatModifier mod in mods.Where(mod => mod.Operation is AddOperation))
        {
            baseValue = mod.Operation.Calculate(baseValue);
        }

        foreach (StatModifier mod in mods.Where(mod => mod.Operation is MultiplyOperation))
        {
            baseValue = mod.Operation.Calculate(baseValue);
        }

        return baseValue;
    }
}