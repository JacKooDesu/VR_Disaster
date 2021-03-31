using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : CustomInteractable
{
    public Vector3 originPoistion;
    public Vector3 originEuler;

    bool inAir = true;
    float airTime;
    float tooFarTime;
    public float airTimeMax = 5;

    public float maxRadius = 2f;

    private void OnEnable()
    {
        originPoistion = transform.position;
        originEuler = transform.eulerAngles;
    }

    private void OnCollisionEnter(Collision other)
    {
        inAir = false;
        airTime = 0;
    }

    private void OnCollisionExit(Collision other)
    {
        inAir = true;
    }

    private void Update()
    {
        if (inAir)
            airTime += Time.deltaTime;

        if ((transform.position - originPoistion).magnitude > maxRadius)
            tooFarTime += Time.deltaTime;
        else
            tooFarTime = 0;

        if (airTime >= airTimeMax || tooFarTime >= airTimeMax)
        {
            transform.eulerAngles = originEuler;
            transform.position = originPoistion;

            GetComponent<Rigidbody>().velocity = Vector3.zero;
            airTime = 0;
            tooFarTime = 0;
        }
    }


}
