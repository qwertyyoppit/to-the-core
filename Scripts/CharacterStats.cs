using System.Collections.Generic

class CharacterStats : MonoBehaviour
{
    [SerializeField] BaseStats _characterBaseStats;

    private List<Stat> _baseStatModifiers = new List<Stat>();

    private Dict<StatType, float> _calculatedStats = new Dict<StatType, float>();

    void Awake()
    {
        
    }

    public void CalculateStats()
    {
        foreach(Stat i in BaseStats)
        {
            _calculatedStats.Add(i.Type, i.Value)
        }
        List<Stat> modifiersToApply = _baseStatModifiers;
        foreach(Stat mod in modifiersToApply)
        {
            if (mod.Modifier == Stat.Modifiers.Add)
            {
                
            }
        }
    }
}