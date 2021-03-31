using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullTorus : Stage
{
    public GameObject torus;
    public UIQuickSetting ui;
    public override void OnBegin()
    {
        base.OnBegin();
        ui.TurnOn();
        StartCoroutine(GameHandler.Singleton.Counter(3f, 3f, delegate { ui.TurnOff(); }));
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnFinish()
    {
        base.OnFinish();
    }
}
