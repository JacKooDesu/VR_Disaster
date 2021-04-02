﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stage : MonoBehaviour
{
    public Transform spawnpoint;  // 重生點，可不用

    // 顯示物件
    public StageObject[] stageObjects;

    [System.Serializable]
    public struct StageObject
    {
        public GameObject obj;
        public int layer;
        public bool destroyOnFinish;
    }

    public GameObject target;  // 目的地

    public bool isFinish = false;   // 是否結束
    public Stage nextStage;     // 下一個Stage

    // Stage開始時
    public virtual void OnBegin()
    {
        // Stage物件顯示
        foreach (StageObject so in stageObjects)
        {
            if (so.layer != -1)
                so.obj.layer = so.layer;

            so.obj.SetActive(true);
        }

        //iTween.MoveTo(GameHandler.Singleton.player.gameObject, spawnpoint, .5f);
    }

    public virtual void OnUpdate()
    {
        if (spawnpoint != null)
            if ((GameHandler.Singleton.player.foot.position - spawnpoint.position).magnitude < .8f)
                GameHandler.Singleton.SetLineGuider(false);

    }

    // Stage結束時
    public virtual void OnFinish()
    {
        // 隱藏所有Stage 物件
        foreach (StageObject so in stageObjects)
            so.obj.SetActive(!so.destroyOnFinish);

        GameHandler.Singleton.SetLineGuider(false);

    }



    // 顯示目標物
    protected void SetTarget(bool b)
    {
        target.SetActive(b);
    }
}
