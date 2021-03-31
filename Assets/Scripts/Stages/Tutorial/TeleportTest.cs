using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTest : Stage
{
    public Valve.VR.InteractionSystem.TeleportPoint point;

    public override void OnBegin()
    {
        if (spawnpoint != null)
            GameHandler.Singleton.SetLineGuider(true, spawnpoint.position);
            
        point.gameObject.SetActive(true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (point.hasTeleported)
        {
            GameHandler.Singleton.StageFinish();
        }
    }

    public override void OnFinish()
    {
        point.gameObject.SetActive(false);
    }
}
