using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class SWave : Stage
{
    public Earthquake earthquake;

    public GameObject roof;
    public int breakChance = 60;

    public Transform tableTop;

    public GameObject waringHUD;

    public Animator elevator;

    public override void OnBegin()
    {
        base.OnBegin();
        GameHandler.Singleton.player.SetCanMove(false);
        earthquake.SetQuake(20f);

        StartCoroutine(GameHandler.Singleton.Counter(
            20,20,
            delegate{
                isFinish = true;
            }
        ));

        BreakRoof();
        StartCoroutine(MakeFog(5f, 40f));

        JacDev.Audio.Earthquake audio = (JacDev.Audio.Earthquake)GameHandler.Singleton.audioHandler;
        audio.ClearSpeaker();
        audio.PlayAudio(audio.SWave, false);
    }

    public override void OnUpdate()
    {
        if (GameHandler.Singleton.cam.transform.position.y > tableTop.position.y)
        {
            if (!waringHUD.activeInHierarchy)
            {
                waringHUD.SetActive(true);
            }
            waringHUD.GetComponent<UIQuickSetting>().TurnOn();
            GameHandler.Singleton.BlurCamera(true);
        }
        else
        {

            waringHUD.GetComponent<UIQuickSetting>().TurnOff();
            GameHandler.Singleton.BlurCamera(false);
        }
    }

    public override void OnFinish()
    {
        //base.OnFinish();
        elevator.SetTrigger("Broken");
        waringHUD.GetComponent<UIQuickSetting>().TurnOff();
        GameHandler.Singleton.cam.GetComponent<BlurOptimized>().enabled = false;
    }

    void BreakRoof()
    {
        foreach (Transform t in roof.transform)
        {
            if (Random.Range(0, 100) < breakChance)
            {
                t.GetComponent<Rigidbody>().isKinematic = false;
                // t.GetComponent<CustomInteractable>().fishRodInteract = true;
            }
        }
    }

    public IEnumerator MakeFog(float time, float fogEnd)
    {
        float t = 0;
        float maxDistance = 1000;
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogEndDistance = maxDistance;

        while (t < time)
        {
            t += Time.deltaTime;

            RenderSettings.fogEndDistance -= ((maxDistance - fogEnd) / time * Time.deltaTime);

            yield return null;
        }
    }
}
