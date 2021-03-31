using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMid : CustomInteractable
{
    List<Transform> targets;

    public void SetTargets(List<Transform> t)
    {
        targets = t;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!interactable.attachedToHand && !interactable.GetComponent<CustomInteractable>().isFishing)
        {
            if (other.transform)
            {
                Transform t;
                for (int i = 0; i < targets.Count; ++i)
                {
                    t = targets[i];
                    if (t == other.transform)
                    {
                        transform.SetPositionAndRotation(other.transform.position, other.transform.rotation);
                        GetComponent<Rigidbody>().isKinematic = true;
                        GetComponent<Collider>().enabled = false;
                        fishRodInteract = false;
                        other.gameObject.SetActive(false);

                        needGoBack = false;

                        if(i+1 < targets.Count){
                            targets[i+1].gameObject.SetActive(true);
                        }else{
                            GameHandler.Singleton.StageFinish();
                        }
                    }
                }
            }
        }
    }
}
