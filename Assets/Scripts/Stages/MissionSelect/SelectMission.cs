﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SelectMission : Stage
{
    public GameObject earthquake;
    public GameObject fireTruck;
    public GameObject flood;
    public GameObject leave;

    public override void OnBegin()
    {
        GameHandler.Singleton.BindEvent(
            earthquake,
            EventTriggerType.PointerEnter,
            delegate
            {
                JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
                audio.PlaySound(audio.earthquakeIntro);
            }
        );

        GameHandler.Singleton.BindEvent(
            earthquake,
            EventTriggerType.PointerExit,
            delegate
            {
                JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
                Destroy(audio.GetSoundAudioSource(audio.earthquakeIntro).gameObject);
            }
        );

        GameHandler.Singleton.BindEvent(
               earthquake,
               EventTriggerType.PointerDown,
               delegate
               {
                   JacDev.Audio.AudioHandler.Singleton.PlaySound(JacDev.Audio.AudioHandler.Singleton.soundList.hover);
                   GameHandler.Singleton.sceneLoader.LoadScene("Earthquake");
                   //GameHandler.Singleton.BlurCamera(false);
               });

        GameHandler.Singleton.BindEvent(
            fireTruck,
            EventTriggerType.PointerEnter,
            delegate
            {
                JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
                audio.PlaySound(audio.fireTruckIntro);
            }
        );

        GameHandler.Singleton.BindEvent(
            fireTruck,
            EventTriggerType.PointerExit,
            delegate
            {
                JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
                Destroy(audio.GetSoundAudioSource(audio.fireTruckIntro).gameObject);
            }
        );

        GameHandler.Singleton.BindEvent(
                fireTruck,
                EventTriggerType.PointerDown,
                delegate
                {
                    JacDev.Audio.AudioHandler.Singleton.PlaySound(JacDev.Audio.AudioHandler.Singleton.soundList.hover);
                    GameHandler.Singleton.sceneLoader.LoadScene("FireTruck");
                    //GameHandler.Singleton.BlurCamera(false);
                });

        // Wait for flood introAudio
        GameHandler.Singleton.BindEvent(
            flood,
            EventTriggerType.PointerEnter,
            delegate
            {
                JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
                audio.PlaySound(audio.floodIntro);
            }
        );

        GameHandler.Singleton.BindEvent(
            flood,
            EventTriggerType.PointerExit,
            delegate
            {
                JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
                Destroy(audio.GetSoundAudioSource(audio.floodIntro).gameObject);
            }
        );

        GameHandler.Singleton.BindEvent(
                flood,
                EventTriggerType.PointerDown,
                delegate
                {
                    JacDev.Audio.AudioHandler.Singleton.PlaySound(JacDev.Audio.AudioHandler.Singleton.soundList.hover);
                    GameHandler.Singleton.sceneLoader.LoadScene("Flood");
                    //GameHandler.Singleton.BlurCamera(false);
                });

        GameHandler.Singleton.BindEvent(
            leave,
            EventTriggerType.PointerEnter,
            delegate
            {
                JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
                audio.PlaySound(audio.soundList.select);
            }
        );

        GameHandler.Singleton.BindEvent(
            leave,
            EventTriggerType.PointerExit,
            delegate
            {
                JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
                Destroy(audio.GetSoundAudioSource(audio.earthquakeIntro).gameObject);
            }
        );

        GameHandler.Singleton.BindEvent(
               leave,
               EventTriggerType.PointerDown,
               delegate
               {
                   Application.Quit();
                   //GameHandler.Singleton.BlurCamera(false);
               });


        // CheckMission("Earthquake", earthquake);
        // CheckMission("FireTruck", fireTruck);
        // CheckMission("Flood", flood);

        JacDev.Audio.TitleScene a = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
        a.PlayAudio(a.bgm, true, GameHandler.Singleton.player.transform).volume = .05f;
    }

    public override void OnUpdate()
    {

    }

    public override void OnFinish()
    {

    }

    public void CheckMission(string name, GameObject objPanel)
    {
        if (GameHandler.Singleton.playerData.GetMissionData(name) != null)
        {
            float t = GameHandler.Singleton.playerData.GetMissionData(name).time;
            int m = Mathf.RoundToInt(t / 60);
            int s = Mathf.RoundToInt(t % 60);

            objPanel.GetComponentInChildren<Text>().text = m.ToString() + " : " + s.ToString();
            objPanel.transform.Find("Stamp").GetComponent<Image>().color = new Color(1f, 0.8078432f, 0f, 0.5f);
        }
        else
        {
            objPanel.GetComponentInChildren<Text>().text = "-- : --";
            objPanel.transform.Find("Stamp").GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        }
    }
}
