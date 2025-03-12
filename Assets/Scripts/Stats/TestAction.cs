using System;
using UnityEngine;

public class TestAction : MonoBehaviour
{
    [System.Serializable]
    public class ActionTrigger
    {
        public Action action;
    }

    public ActionTrigger trigger;
}
