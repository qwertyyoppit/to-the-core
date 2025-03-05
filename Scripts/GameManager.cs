using System;
using TMPro;
using UnityEngine;

public class GameManager : ScriptableObject
{

	public static GameManager instance;

    private void OnEnable()
    {
        if (instance == null)
			instance = this;
		else
			Destroy(instance); 
    }
}
