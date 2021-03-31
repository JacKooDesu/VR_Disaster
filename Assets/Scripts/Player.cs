using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using Valve.VR;

public class Player : MonoBehaviour
{
    public bool isStop = true;
    float stopTime = 3f;

    public float speed;
    bool gettingUp;
    Rigidbody rb;

    public Canvas canvas;

    GameObject target;
    public bool hasTarget;  //是否有目標物

    public Transform teleportArea;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
    }

    public void SetTarget(GameObject target)
    {
        hasTarget = true;
        this.target = target;
    }

    public void SetCanMove(bool b)
    {
        if (teleportArea.transform.childCount != 0)
        {
            foreach (Transform t in teleportArea.transform)
            {
                t.GetComponent<Valve.VR.InteractionSystem.TeleportArea>().locked = !b;
            }
        }
        else
        {
            teleportArea.GetComponent<Valve.VR.InteractionSystem.TeleportArea>().locked = !b;
        }
    }

    public IEnumerator WaitHandOverHead(UnityEngine.Events.UnityAction nextAction)
    {
        Transform leftHand = GetComponentInChildren<Valve.VR.InteractionSystem.Player>().leftHand.transform;
        Transform rightHand = GetComponentInChildren<Valve.VR.InteractionSystem.Player>().rightHand.transform;
        Transform camPos = GameHandler.Singleton.cam.transform;
        while (
            (leftHand.position.y < camPos.position.y ||
            rightHand.position.y < camPos.position.y) ||
            (leftHand.position - camPos.position).magnitude > .5f ||
            (rightHand.position - camPos.position).magnitude > .5f
            )
        {
            print("not put on head");
            yield return null;
        }

        nextAction.Invoke();
    }
}
