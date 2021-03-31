using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : CustomInteractable
{
    public Transform safePlace;
    public bool isSafe = false;
    
    private void OnTriggerEnter(Collider other) {
        if(other.transform){
            if(other.transform == safePlace)
                isSafe = true;
        }
    }
}
