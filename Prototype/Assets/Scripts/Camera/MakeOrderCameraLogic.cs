using UnityEngine;
using System.Collections;

public class MakeOrderCameraLogic : MonoBehaviour
{
    Vector3 OriginalPosition;
    Vector3 OriginalRotation;
    Vector3 Speed;

    [Range(0.01f, 0.99f)]
    public float Deaccelerate = 0.01f;

    public Vector2 XBoundary;

    void Start ()
    {
        OriginalPosition = transform.position;
        OriginalRotation = transform.rotation.eulerAngles;
    }

    public void Slide (Vector3 dP)
    {
        Speed = dP;
    }

    void Update ()
    {
        Vector3 pos = transform.position;
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

        transform.position = pos;
    }

    public void Return ()
    {
        transform.position = OriginalPosition;
        transform.rotation = Quaternion.Euler(OriginalRotation);
    }

}
