using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.Events;

[RequireComponent(typeof(Interactable))]
public class CustomInteractable : MonoBehaviour
{
    private Vector3 oldPosition;
    private Quaternion oldRotation;

    private float attachTime;

    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);

    // 取得Interactabe腳本
    protected Interactable interactable;

    // 是否可以用釣竿拉起
    public bool fishRodInteract = false;
    public bool fishRodPullToHand = false;

    public UnityEvent onHoverBegin;
    public UnityEvent onHover;
    public UnityEvent onHoverUpdate;
    public UnityEvent onHoverExit;
    public UnityEvent onAttachHand;
    public UnityEvent onDetachHand;

    public bool isFishing;
    public bool isGrabing;

    Vector3 originPosition;
    public Transform activeArea;
    protected bool needGoBack = true;

    private void Awake()
    {
        interactable = this.GetComponent<Interactable>();
        originPosition = transform.position;
    }

    private void HandHoverUpdate(Hand hand)
    {
        CheckGrab(hand);

        onHoverUpdate.Invoke();
    }

    public void CheckGrab(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            // Save our position/rotation so that we can restore it when we detach
            oldPosition = transform.position;
            oldRotation = transform.rotation;

            // Call this to continue receiving HandHoverUpdate messages,
            // and prevent the hand from hovering over anything else
            hand.HoverLock(interactable);

            // Attach this object to the hand
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);

            isFishing = false;

            isGrabing = true;

        }
        else if (isGrabEnding)
        {
            // Detach this object from the hand
            hand.DetachObject(gameObject);

            // Call this to undo HoverLock
            hand.HoverUnlock(interactable);

            if (GetComponent<Rigidbody>())
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.isKinematic = false;
            }

            // Restore position/rotation
            //transform.position = oldPosition;
            //transform.rotation = oldRotation;
            transform.SetParent(GameHandler.Singleton.ObjectParent);

            isGrabing = false;
        }
    }


    private void OnHandHoverBegin()
    {
        onHoverBegin.Invoke();
    }


    //-------------------------------------------------
    private void OnHandHoverEnd()
    {
        onHoverExit.Invoke();
    }


    //-------------------------------------------------
    private void OnAttachedToHand(Hand hand)
    {
        onAttachHand.Invoke();
    }


    //-------------------------------------------------
    private void OnDetachedFromHand(Hand hand)
    {
        onDetachHand.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (activeArea != null && needGoBack)
        {
            if (other.transform == activeArea)
            {
                StartCoroutine(GoBack());
            }
        }
    }

    IEnumerator GoBack()
    {
        while (isFishing || isGrabing)
        {
            yield return null;
        }

        transform.position = originPosition;
    }
}
