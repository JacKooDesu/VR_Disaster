using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstalGateMid : Stage
{
    public Transform midTarget;
    public Transform midObject;

    public override void OnBegin()
    {
        midTarget.gameObject.SetActive(true);

        List<Transform> targets = new List<Transform>();
        foreach (Transform t in midTarget)
        {
            targets.Add(t);
            t.gameObject.SetActive(false);
        }

        midTarget.GetChild(0).gameObject.SetActive(true);

        foreach (Transform t in midObject)
        {
            //t.GetComponent<GateMid>().fishRodInteract = true;
            t.GetComponent<GateMid>().SetTargets(targets);
        }

        JacDev.Audio.Flood a = (JacDev.Audio.Flood)GameHandler.Singleton.audioHandler;
        a.PlaySound(a.instalGateMid);
    }

    public override void OnFinish()
    {
        base.OnFinish();
        midTarget.gameObject.SetActive(false);

        
    }
}
