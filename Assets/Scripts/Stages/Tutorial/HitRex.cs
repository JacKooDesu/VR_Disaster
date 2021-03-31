using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRex : Stage
{
    public Valve.VR.InteractionSystem.TeleportPoint point;

    public override void OnBegin()
    {
        JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
        audio.PlaySound(audio.hammerIntro);

        if (spawnpoint != null)
            GameHandler.Singleton.SetLineGuider(true, spawnpoint.position);

        point.gameObject.SetActive(true);
    }

    public override void OnFinish()
    {
        point.gameObject.SetActive(false);
    }
}

