using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : Stage
{
    public UIQuickSetting[] UIs;
    public override void OnBegin()
    {
        base.OnBegin();
        GameHandler.Singleton.player.SetCanMove(false);
        float t = 0;
        float interval = 1f;

        JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;

        // AudioClip[] clips = new AudioClip[]{
        //     audio.soundList.slogan,
        //     audio.welcome,
        //     audio.controllerFrontIntro1,
        //     audio.controllerFrontIntro2
        // };

        StartCoroutine(GameHandler.Singleton.Counter(t, t, delegate { audio.PlaySound(audio.soundList.slogan); }));
        StartCoroutine(GameHandler.Singleton.Counter(t, t, delegate { UIs[0].TurnOn(); }));
        float t1 = t + audio.soundList.slogan.length + interval;

        StartCoroutine(GameHandler.Singleton.Counter(t1, t1, delegate { UIs[0].TurnOff(); }));


        StartCoroutine(GameHandler.Singleton.Counter(t1, t1, delegate
        {
            AudioSource a = audio.PlaySound(audio.bgm);
            a.loop = true;
            a.volume = .2f;

            audio.PlaySound(audio.welcome);
        }));
        StartCoroutine(GameHandler.Singleton.Counter(t1, t1, delegate { UIs[1].TurnOn(); }));
        float t2 = t1 + audio.welcome.length + interval;

        StartCoroutine(GameHandler.Singleton.Counter(t2, t2, delegate { UIs[1].TurnOff(); }));


        StartCoroutine(GameHandler.Singleton.Counter(t2, t2, delegate { audio.PlaySound(audio.controllerFrontIntro1); }));
        StartCoroutine(GameHandler.Singleton.Counter(t2, t2, delegate { UIs[2].TurnOn(); }));
        float t3 = t2 + audio.controllerFrontIntro1.length + interval;
        StartCoroutine(GameHandler.Singleton.Counter(t3, t3, delegate { audio.PlaySound(audio.controllerFrontIntro2); }));
        float t4 = t3 + audio.controllerFrontIntro2.length + interval;

        StartCoroutine(GameHandler.Singleton.Counter(t4, t4, delegate { UIs[2].TurnOff(); }));


        StartCoroutine(GameHandler.Singleton.Counter(t4, t4, delegate { audio.PlaySound(audio.contorllerRearIntro1); }));
        StartCoroutine(GameHandler.Singleton.Counter(t4, t4, delegate { UIs[3].TurnOn(); }));
        float t5 = t4 + audio.contorllerRearIntro1.length + interval;
        StartCoroutine(GameHandler.Singleton.Counter(t5, t5, delegate { audio.PlaySound(audio.contorllerRearIntro2); }));

        float t6 = t5 + audio.contorllerRearIntro2.length + interval;
        StartCoroutine(GameHandler.Singleton.Counter(t6, t6, delegate { UIs[3].TurnOff(); }));
        StartCoroutine(
            GameHandler.Singleton.Counter(
                t6,
                t6,
                delegate
                {
                    if (GameHandler.Singleton.GetCurrentStage() == this)
                        GameHandler.Singleton.StageFinish();
                }
                )
            );
    }

    public override void OnFinish()
    {
        base.OnFinish();
    }
}
