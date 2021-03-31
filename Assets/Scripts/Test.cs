using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Stage
{
    public ObjectTweener objectTweener;

    private void Start() {
        objectTweener.MoveToPoint(0);
    }

    public void MoveNext(){
        objectTweener.MoveNextPoint();
    }
}
