using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecRainMeter : Stage
{
    public CustomInteractable interactArea;
    public GameObject ui;

    public override void OnBegin()
    {
        interactArea.gameObject.SetActive(true);
        interactArea.onHoverBegin.AddListener(
            delegate { 
                StartCoroutine(ShowUI());
                interactArea.gameObject.SetActive(false);
            }
        );
    }

    IEnumerator ShowUI()
    {
        /*
        ui.SetActive(true);
        ui.GetComponentInChildren<UIQuickSetting>().TurnOn();

        JacDev.Audio.Flood a = (JacDev.Audio.Flood)GameHandler.Singleton.audioHandler;
        a.PlaySound(a.specWaterMeter);

        yield return new WaitForSeconds(a.specWaterMeter.length);

        ui.GetComponentInChildren<UIQuickSetting>().TurnOff();
        */

        GameHandler.Singleton.StageFinish();
        yield return null;
    }
}
