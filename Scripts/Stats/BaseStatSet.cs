using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BaseStatSet")]
public class BaseStatSet : ScriptableObject
{
    public List<Stat> StatList;
}