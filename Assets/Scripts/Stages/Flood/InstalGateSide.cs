using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstalGateSide : Stage
{
    public Transform sideTarget;
    public Transform sideObject;

    public override void OnBegin()
    {
        base.OnBegin();
        sideTarget.gameObject.SetActive(true);

        List<Transform> targets = new List<Transform>();
        foreach(Transform t in sideTarget){
            targets.Add(t);
        }

        foreach(Transform t in sideObject){
            t.GetComponent<GateSide>().SetTargets(targets);
        }

        JacDev.Audio.Flood a = (JacDev.Audio.Flood)GameHandler.Singleton.audioHandler;
        a.PlaySound(a.instalGateSide);
    }

    public override void OnFinish()
    {
        sideTarget.gameObject.SetActive(false);

        
    }
}
