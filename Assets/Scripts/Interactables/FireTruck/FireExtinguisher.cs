using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class FireExtinguisher : CustomInteractable
{
    public Hand holdHand;
    public Hand anotherHand;

    public Transform sprayParticle;

    public SteamVR_Input_Sources inputTarget;
    public SteamVR_Action_Boolean sprayButton;

    bool hasTakeOnce = false;

    private void OnAttachedToHand(Hand hand)
    {
        if(!hasTakeOnce){
            hasTakeOnce = true;
            GameHandler.Singleton.StageFinish();
        }
            
            
        holdHand = hand;
        anotherHand = hand.otherHand;

        inputTarget = hand.handType;

        sprayParticle.SetParent(anotherHand.transform);

        sprayParticle.localPosition = Vector3.zero;
        sprayParticle.localEulerAngles = Vector3.zero;

        CheckSpray();
    }

    public void CheckSpray()
    {
        StartCoroutine(Spray());
    }

    IEnumerator Spray()
    {
        sprayParticle.gameObject.SetActive(true);
        while (interactable.attachedToHand)
        {
            JacDev.Audio.FireTruck audio = (JacDev.Audio.FireTruck)GameHandler.Singleton.audioHandler;
            if (sprayButton.GetState(inputTarget))
            {
                if(!sprayParticle.GetComponent<ParticleSystem>().isPlaying){
                    sprayParticle.GetComponent<ParticleSystem>().Play();
                }
                
                audio.PlayAudio(audio.extinguisher, false, transform);
            }
            //sprayParticle.gameObject.SetActive(true);
            else
            {
                sprayParticle.GetComponent<ParticleSystem>().Stop();
                if(GetComponentInChildren<AudioSource>())
                    Destroy(GetComponentInChildren<AudioSource>().gameObject);
            }

            //sprayParticle.gameObject.SetActive(false);

            yield return null;
        }

        sprayParticle.gameObject.SetActive(false);
        sprayParticle.SetParent(null);
        anotherHand = null;
    }
}
