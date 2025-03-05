using UnityEngine;

public class PingasItemScript : BaseItemScript
{
    public override void UpdateStats()
    {
        base.UpdateStats();
        controller.jumpMultiplier = 5.0f;
    }
}
