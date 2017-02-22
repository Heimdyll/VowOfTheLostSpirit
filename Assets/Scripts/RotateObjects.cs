using UnityEngine;
using System.Collections;

public class RotateObjects : MonoBehaviour {


    public float waterLevel = 4;
    public float floatHight = 2;
    public float bounceDamp = 0.05f;
    public Vector3 bouyancyCenterOffset;

    float forceFactor;
    Vector3 actionPoint;
    Vector3 upLift;

    void Start()
    {

    }

    void Update()
    {
        actionPoint = transform.position + transform.TransformDirection(bouyancyCenterOffset);
        forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHight);

        if (forceFactor > 0f)
        {
            upLift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
            GetComponent<Rigidbody>().AddForceAtPosition(upLift, actionPoint);
        }
    }
}
