using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffGas : Stage
{
    public CustomInteractable interactArea;
    public GameObject fire;

    public override void OnBegin()
    {
        base.OnBegin();

        if (spawnpoint != null)
            GameHandler.Singleton.SetLineGuider(true, spawnpoint.position);

        interactArea.gameObject.SetActive(true);

        interactArea.onHoverBegin.AddListener(
            delegate
            {
                GameHandler.Singleton.StageFinish();
                interactArea.gameObject.SetActive(false);
            }
        );

        JacDev.Audio.Flood a = (JacDev.Audio.Flood)GameHandler.Singleton.audioHandler;
        AudioSource boil = a.PlaySound(a.boilWater);
        StartCoroutine(GameHandler.Singleton.Counter(5, delegate
       {
           boil.volume = .4f;
           a.PlaySound(a.turnOffGas);
       }));

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnFinish()
    {
        fire.SetActive(false);
    }
}
