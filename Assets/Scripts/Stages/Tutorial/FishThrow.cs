using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishThrow : Stage
{
    public Valve.VR.InteractionSystem.TeleportPoint point;
    public GlassController glass;

    public override void OnBegin()
    {
        JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
        audio.PlaySound(audio.chairIntro);
        glass.BindGlass();

        if (spawnpoint != null)
            GameHandler.Singleton.SetLineGuider(true, spawnpoint.position);

        point.gameObject.SetActive(true);
    }

    public override void OnFinish()
    {
        JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
        audio.PlaySound(audio.great);
        point.gameObject.SetActive(false);
    }
}

