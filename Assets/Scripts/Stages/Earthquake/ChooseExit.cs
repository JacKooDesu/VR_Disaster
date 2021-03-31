using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseExit : Stage
{
    public UIQuickSetting failedUI;

    Vector3 originPlayerPosition;

    public Animator[] exits;

    public override void OnBegin()
    {
        base.OnBegin();
        originPlayerPosition = GameHandler.Singleton.player.transform.position;

        GameHandler.Singleton.player.SetCanMove(true);

        JacDev.Audio.Earthquake audio = (JacDev.Audio.Earthquake)GameHandler.Singleton.audioHandler;
        audio.PlaySound(audio.protectHead);
    }

    public override void OnUpdate()
    {
        // foreach (StageObject so in stageObjects)
        // {
        //     if (so.obj.GetComponentInChildren<TeleportPoint>().hasTeleported)
        //     {
        //         if (so.obj.GetComponentInChildren<TeleportPoint>().title == "ELEVATOR")
        //         {
        //             if (!so.obj.activeInHierarchy)
        //                 return;

        //             so.obj.SetActive(false);
        //             if (!failedUI.gameObject.activeInHierarchy)
        //                 failedUI.gameObject.SetActive(true);

        //             failedUI.TurnOn();
        //             GameHandler.Singleton.BlurCamera(true);
        //             StartCoroutine(failedUI.WaitStatusChange(
        //                 delegate
        //                 {
        //                     GameHandler.Singleton.BlurCamera(false);
        //                     GameHandler.Singleton.player.transform.position = originPlayerPosition;

        //                     so.obj.GetComponentInChildren<TeleportPoint>().ResetTeleportStatus();
        //                 }
        //             ));

        //         }

        //         else if (so.obj.GetComponentInChildren<TeleportPoint>().title == "EXIT")
        //         {
        //             GameHandler.Singleton.StageFinish();
        //         }
        //     }
        // }
    }

    public override void OnFinish()
    {
        base.OnFinish();
        foreach (Animator a in exits)
        {
            a.enabled = true;
        }
    }
}
