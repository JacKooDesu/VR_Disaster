using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeBomb : Stage
{
    public override void OnBegin()
    {
        base.OnBegin();
        JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
        audio.PlaySound(audio.takeBomb);
    }

    public override void OnFinish()
    {
        base.OnFinish();
    }
}
