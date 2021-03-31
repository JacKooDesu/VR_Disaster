using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TRex : MonoBehaviour
{
    public Material mainMaterial;
    public ParticleSystem particle;

    public bool isSpraying = false;
    bool hasGetHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponentInParent<Hand>())
        {
            Vector3 vel = other.GetComponentInParent<Hand>().trackedObject.GetVelocity();
            JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
            audio.PlayAudio(audio.rexGetHit, false, transform);

            if (vel.magnitude > 2f && !isSpraying)
            {
                print(vel.magnitude);
                StartCoroutine(ChangeColor(vel.magnitude - 2));

                audio.PlayAudio(audio.rexSprayFire, false, transform);
            }

        }
    }

    IEnumerator ChangeColor(float value)
    {
        isSpraying = true;
        Color origin = new Color(mainMaterial.color.r, mainMaterial.color.g, mainMaterial.color.b, mainMaterial.color.a);
        float t = 0;

        float unit = value / 10;

        while (t < value)
        {
            mainMaterial.SetColor(
                "_Color",
                Color.Lerp(
                    origin,
                    Color.red,
                    t * unit));

            t += .05f;
            yield return null;
        }


        particle.Play();

        while (particle.isPlaying)
        {
            yield return null;
        }

        while (t > 0)
        {
            mainMaterial.SetColor("_Color", Color.Lerp(origin, Color.red, t * unit));
            t -= .05f;
            yield return null;
        }

        isSpraying = false;

        if (!hasGetHit)
        {
            JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
            audio.PlaySound(audio.hammerComplete);
            StartCoroutine(
                GameHandler.Singleton.Counter(
                    audio.hammerComplete.length + 1,
                    audio.hammerComplete.length + 1,
                    delegate { GameHandler.Singleton.StageFinish(); }));

            hasGetHit = true;
        }
    }
}
