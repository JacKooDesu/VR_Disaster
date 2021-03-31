using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Glass : MonoBehaviour
{
    Transform origin;
    Transform broken;
    GlassController glassController;

    float breakForce = .8f;

    public void SetGlass(Transform origin, Transform broken, GlassController g)
    {
        this.origin = origin;
        this.broken = broken;
        this.glassController = g;
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (other.transform.GetComponentInParent<Hand>())
        {
            Hand h = other.transform.GetComponentInParent<Hand>();
            print(h.trackedObject.GetVelocity().magnitude);

            if (h.trackedObject.GetVelocity().magnitude > breakForce)
            {
                GlassBroken();
            }
        }
        else if (other.GetComponentInParent<Rigidbody>())
        {
            Rigidbody rb = other.GetComponentInParent<Rigidbody>();
            if (rb.velocity.magnitude > breakForce)
                GlassBroken();
        }
        else if (other.GetComponent<Rigidbody>())
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb.velocity.magnitude > breakForce)
                GlassBroken();
        }

    }

    void GlassBroken()
    {
        JacDev.Audio.AudioHandler audio = GameHandler.Singleton.audioHandler;
        audio.PlayAudio(audio.soundList.glassBreak, false, transform);
        broken.gameObject.SetActive(true);

        origin.gameObject.SetActive(false);

        glassController.brokenAction.Invoke();
    }
}
