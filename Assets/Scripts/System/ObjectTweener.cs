using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectTweener  // 物件位移類別
{

    public Transform target;
    public int currentPoint = 0;

    public Transform[] points;

    public void SetTarget(Transform t)  // 綁定位移物件
    {
        target = t;
    }

    public void MoveNextPoint()     // 尚未定義完整
    {

    }

    public void MoveToPoint(int p)  // 位移至定點
    {
        iTween.MoveTo(target.gameObject, points[p].position, .5f);
        iTween.RotateTo(target.gameObject, points[p].eulerAngles, .5f);
    }

    public void AddPoint()      // Editor mode function
    {

    }
}
