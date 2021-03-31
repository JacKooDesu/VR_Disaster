using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    public float defaultLength = 5f;
    public GameObject dot;
    public VRInputModule inputModule;

    public float offset = .1f;

    LineRenderer lineRenderer = null;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLine();
    }

    void UpdateLine()
    {
        PointerEventData data = inputModule.GetData();

        float targetLength = data.pointerCurrentRaycast.distance == 0 ? defaultLength : data.pointerCurrentRaycast.distance;
        RaycastHit hit = CreateRaycast(targetLength);

        Vector3 endPosition = transform.position + (transform.forward * (targetLength+offset));

        if(data.pointerCurrentRaycast.distance == 0){
            lineRenderer.enabled = false;
            dot.SetActive(false);
        }
        else{
            lineRenderer.enabled = true;
            dot.SetActive(true);
        }
            

        // if (hit.collider != null)
        // {
        //     endPosition = hit.point;
        // }
        

        dot.transform.position = endPosition;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPosition);
    }

    RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength);

        return hit;
    }
}
