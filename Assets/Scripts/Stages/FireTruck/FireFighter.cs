using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFighter : Stage
{
    public float minTime;
    public float maxTime;

    public UIQuickSetting ui1;
    public UIQuickSetting ui2;
    public UIQuickSetting ui3;

    public Transform fireSpot;

    bool hasAim = false;

    public override void OnBegin()
    {
        //base.OnBegin();

        ui1.TurnOn();
        StartCoroutine(GameHandler.Singleton.Counter(3f, 3f, delegate
        {
            ui1.TurnOff();
        }));

    }

    public override void OnUpdate()
    {
        if (!hasAim)
        {
            Valve.VR.InteractionSystem.Player p = GameHandler.Singleton.player.GetComponentInChildren<Valve.VR.InteractionSystem.Player>();
            Transform origin = null;
            if (p.leftHand.currentAttachedObject)
                origin = p.rightHand.transform;
            else if (p.rightHand.currentAttachedObject)
                origin = p.leftHand.transform;

            RaycastHit hit;
            if (origin != null)
            {
                Ray ray = new Ray(origin.position, origin.forward);
                if (Physics.Raycast(ray, out hit, 10f))
                {
                    print(hit.transform.name);
                    if (hit.transform == fireSpot)
                    {
                        AimAndStrafy();
                        hasAim = true;
                    }


                }
            }

        }

    }

    void AimAndStrafy()
    {
        ui2.TurnOn();
        StartCoroutine(GameHandler.Singleton.Counter(3f, 3f, delegate
        {
            ui2.TurnOff();
            ui3.TurnOn();
        }));

        StartCoroutine(GameHandler.Singleton.Counter(6f, 6f, delegate { ui3.TurnOff(); }));
    }

    public override void OnFinish()
    {
        base.OnFinish();
     }
}
