using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyingID : Stage
{
    public override void OnBegin()
    {
        base.OnBegin();
        GameHandler.Singleton.player.SetCanMove(false);
        GameHandler.Singleton.GrayCamera(true);
    }

    public override void OnFinish()
    {
        base.OnFinish();
        GameHandler.Singleton.player.SetCanMove(true);
        GameHandler.Singleton.GrayCamera(false);
    }
}
