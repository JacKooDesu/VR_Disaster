using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class FishRodSelector : MonoBehaviour
{
    LineRenderer lineRenderer;
    public SteamVR_Input_Sources inputTarget;
    public SteamVR_Action_Single fishRodSelect;

    public Transform rodEnd;
    public Transform rodEndOrigin;

    public Transform fishingObject;
    public Transform objParent;

    Hand hand;

    public Material canFishMaterial;
    public Material fishingMaterial;

    [SerializeField] bool isFishing = false;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        hand = GetComponentInChildren<Hand>();
    }
    // Update is called once per frame
    void Update()
    {
        // print(hand.AttachedObjects.Count);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 20f))
        {
            if (hit.transform.GetComponent<CustomInteractable>() && (hit.point - transform.position).magnitude > .5f)
            {
                lineRenderer.enabled = true;
                if (fishRodSelect.GetAxis(inputTarget) > .5f && !isFishing && hit.transform.GetComponent<CustomInteractable>().fishRodInteract)
                {
                    isFishing = true;

                    fishingObject = hit.transform;
                    rodEnd.position = hit.point;
                    rodEndOrigin.position = hit.point;

                    objParent = fishingObject.parent;

                    fishingObject.SetParent(rodEnd);

                    // fishingObject.GetComponent<Rigidbody>().isKinematic = true;
                    fishingObject.GetComponent<CustomInteractable>().isFishing = true;

                    StartCoroutine(Fishing());
                }
            }
            else if (!isFishing)
            {
                lineRenderer.positionCount = 0;
                lineRenderer.enabled = false;
            }
        }
        else if (!isFishing)
        {
            lineRenderer.positionCount = 0;
            lineRenderer.enabled = false;
        }


        if (isFishing)
        {
            lineRenderer.material = fishingMaterial;
            CurveLine(100);
        }
        else
        {
            lineRenderer.material = canFishMaterial;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(new Vector3[2] { transform.position, hit.point });
        }
    }

    void CurveLine(float pointCount)
    {
        List<Vector3> points = new List<Vector3>();
        Vector3 p1 = transform.position;
        Vector3 p2 = rodEnd.position;
        Vector3 p3 = rodEndOrigin.position;
        Vector3 p12 = p2 - p1;
        float length = p12.magnitude;

        for (float i = 0; i < 1; i += (1 / pointCount))
        {
            Vector3 t1 = Vector3.Lerp(p1, p2, i);
            Vector3 t2 = Vector3.Lerp(p1, p3, i);

            Vector3 curve = Vector3.Lerp(t2, t1, i);

            points.Add(curve);
        }

        lineRenderer.positionCount = (int)pointCount;
        lineRenderer.SetPositions(points.ToArray());
    }

    IEnumerator Fishing()
    {
        hand.enabled = false;
        fishingObject.GetComponent<Rigidbody>().isKinematic = true;

        // 2021.03.09 update
        while (fishRodSelect.GetAxis(inputTarget) > 0.5f && fishingObject.GetComponent<CustomInteractable>().isFishing)
        {
            if (fishRodSelect.GetAxis(inputTarget) == 1 && fishingObject.GetComponent<CustomInteractable>().fishRodPullToHand)
            {
                while ((rodEndOrigin.position - transform.position).magnitude > .1f && fishingObject.GetComponent<CustomInteractable>().isFishing)
                {
                    rodEndOrigin.position = Vector3.Lerp(rodEndOrigin.position, transform.position, .1f);
                    yield return null;
                }
            }
            yield return null;
        }


        fishingObject.GetComponent<Rigidbody>().isKinematic = false;
        Rigidbody r = fishingObject.GetComponent<Rigidbody>();
        r.velocity = rodEnd.GetComponent<Rigidbody>().velocity;

        fishingObject.GetComponent<CustomInteractable>().isFishing = false;


        rodEnd.DetachChildren();

        fishingObject.SetParent(objParent);
        objParent = null;

        isFishing = false;

        hand.enabled = true;

        lineRenderer.enabled = false;
    }

    // 2021.03.09 update
    public void EndFishing()
    {
        isFishing = false;
    }
}
