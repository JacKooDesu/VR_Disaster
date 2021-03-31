using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGlass : MonoBehaviour
{
    public Material originMaterial;
    public Material hintMaterial;

    public GlassController glassController;

    private void OnTriggerEnter(Collider other)
    {
        JacDev.Audio.AudioHandler audio = GameHandler.Singleton.audioHandler;
        audio.PlayAudio(audio.soundList.glassBreak, false, transform);
        
        GetComponent<Rigidbody>().isKinematic = false;

        if (other.GetComponent<Rigidbody>())
        {
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().velocity = other.GetComponent<Rigidbody>().velocity * 5f;
            }
        }
        else if (other.GetComponentInParent<Rigidbody>())
        {
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().velocity = other.GetComponentInParent<Rigidbody>().velocity * 5f;

            }
        }
        glassController.AddBreakGlass();
        StartCoroutine(GameHandler.Singleton.Counter(3f, 3f, delegate { Destroy(gameObject); }));
    }

    public void BindGlassController(GlassController g){
        glassController = g;
    }
}
