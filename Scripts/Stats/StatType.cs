using UnityEngine;

[CreateAssetMenu(fileName = "StatType", menuName = "Scriptable Objects/StatType")]
public class StatType : ScriptableObject
{
    private void OnEnable()
    {
        Stat.Types.Add(name);
    }
}
