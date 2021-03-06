﻿using UnityEngine;
using System.Collections;

public class MakeOrderCameraLogic : MonoBehaviour
{
    Vector3 OriginalPosition;
    Vector3 OriginalRotation;
    Vector3 TargetPosition;
    bool MoveToTarget;
    Vector3 Speed;

    [Range(0.01f, 0.99f)]
    public float Deaccelerate = 0.01f;

    public Vector2 XBoundary;

    void Awake ()
    {
        OriginalPosition = transform.position;
        OriginalRotation = transform.rotation.eulerAngles;
    }

    public void Slide (Vector3 dP)
    {
        MoveToTarget = false;
        Speed = dP;
    }

    void Update ()
    {
        Vector3 pos = transform.position;

        if(Time.timeScale != 0)
        {
            if (MoveToTarget)
            {
                pos += (TargetPosition - pos) * Time.deltaTime * 5;
            }
            else
            {
                Speed *= Deaccelerate;
                pos += Speed;
                //X Boundary
                if (pos.x <= XBoundary.x)
                {
                    pos.x = XBoundary.x;
                    Speed.x = 0;
                }
                else if (pos.x >= XBoundary.y)
                {
                    pos.x = XBoundary.y;
                    Speed.x = 0;
                }
            }
        }

        transform.position = pos;
    }

    public void Return ()
    {
        transform.position = OriginalPosition;
        MoveToTarget = false;
        transform.rotation = Quaternion.Euler(OriginalRotation);
    }

    public void SetTargetLocation (Vector3 position)
    {
        MoveToTarget = true;
        TargetPosition = position;
    }
}
