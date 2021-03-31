using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : CustomInteractable
{
    public Transform[] Joints;

    Vector3 originPosition;

    bool isFiring = false;
    int currentJoint = 0;
    float time;
    float speed = .1f;
    public Transform fireParticle;
    public Transform explosiveParticle;

    bool hasThrowOnce = false;

    private void OnEnable()
    {
        originPosition = transform.position;
    }

    public void FireLine()
    {
        isFiring = true;
        fireParticle.GetComponent<ParticleSystem>().Play();

        if (!hasThrowOnce)
        {
            JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
            audio.PlaySound(audio.bombIntro);
        }

    }

    public void ThrowOut()
    {
        if (!hasThrowOnce)
        {
            hasThrowOnce = true;
            StartCoroutine(GameHandler.Singleton.player.WaitHandOverHead(
                delegate { 
                    Explosive();
                    GameHandler.Singleton.StageFinish();    
                }));
        }
        else
        {
            Explosive();
        }

    }

    void Explosive()
    {
        StartCoroutine(GameHandler.Singleton.Counter(3f, 3f, delegate
                    {
                        explosiveParticle.GetComponent<ParticleSystem>().Play();
                        JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
                        audio.PlayAudio(audio.explosive, false, transform);
                        isFiring = false;
                        currentJoint = 0;
                        time = 0;
                    }));

        StartCoroutine(GameHandler.Singleton.Counter(4f, 4f, delegate
        {
            fireParticle.GetComponent<ParticleSystem>().Stop();
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = originPosition;
            GetComponent<Rigidbody>().useGravity = false;
        }));
    }

    private void Update()
    {
        JacDev.Audio.TitleScene audio = (JacDev.Audio.TitleScene)GameHandler.Singleton.audioHandler;
        if (isFiring)
        {
            audio.PlayAudio(audio.fuse, false, transform);
            if (time >= 1)
            {
                if (currentJoint < Joints.Length - 2)
                {
                    currentJoint += 1;
                    time = 0;
                }
            }
            else
                time += Time.deltaTime;

            fireParticle.position = Vector3.Lerp(Joints[currentJoint].position, Joints[currentJoint + 1].position, time * speed);
        }
        else
        {
            if (audio.GetSpeakerAudioSource(audio.fuse))
                Destroy(audio.GetSpeakerAudioSource(audio.fuse).gameObject);
        }

    }
}
