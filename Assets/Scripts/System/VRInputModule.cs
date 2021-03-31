using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.EventSystems;

public class VRInputModule : BaseInputModule
{
    public Camera cam;
    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Boolean clickAction;

    GameObject currentObject = null;
    PointerEventData data = null;

    protected override void Awake()
    {
        base.Awake();

        data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        // reset and set camera
        data.Reset();
        data.position = new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2);

        // raycast
        eventSystem.RaycastAll(data, m_RaycastResultCache);
        data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        currentObject = data.pointerCurrentRaycast.gameObject;

        // clear
        m_RaycastResultCache.Clear();

        // hover
        HandlePointerExitAndEnter(data, currentObject);

        // press
        if (clickAction.GetStateDown(targetSource))
            ProcessPress(data);

        // release
        if (clickAction.GetStateUp(targetSource))
            ProcessRelease(data);
    }

    void ProcessPress(PointerEventData data)
    {
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.pointerDownHandler);

        if (newPointerPress == null)
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = currentObject;
    }

    void ProcessRelease(PointerEventData data)
    {
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        if (data.pointerPress == pointerUpHandler)
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);

        eventSystem.SetSelectedGameObject(null);

        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;

    }

    public PointerEventData GetData(){
        return data;
    }
}
