using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private BaseStats baseStats;
    public Stats Stats { get; private set; }

    private void Awake()
    {
        Stats = new Stats(baseStats);
    }
}