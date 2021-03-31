using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSide : CustomInteractable
{
    public List<Transform> targets = new List<Transform>();

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
                foreach (Transform t in targets)
                {
                    if (t == other.transform)
                    {
                        transform.SetPositionAndRotation(other.transform.position, other.transform.rotation);
                        GetComponent<Rigidbody>().isKinematic = true;
                        fishRodInteract = false;
                        other.gameObject.SetActive(false);

                        needGoBack = false;
                    }
                }

                foreach(Transform t in targets){
                    if(t.gameObject.activeInHierarchy)
                        return;
                }
                GameHandler.Singleton.StageFinish();
            }
        }

    }
}
