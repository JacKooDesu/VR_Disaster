using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class SceneHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;

    private void Awake() {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerClick += PointerClick;
        laserPointer.PointerOut += PointerOutside;
    }

    public void PointerClick(object sender, PointerEventArgs e){
        // when click
        print(e.target);

    }

    public void PointerInside(object sender,PointerEventArgs e){
        // when pointer inside
    }

    public void PointerOutside(object sender, PointerEventArgs e){
        // when pointer outside
    }
}
