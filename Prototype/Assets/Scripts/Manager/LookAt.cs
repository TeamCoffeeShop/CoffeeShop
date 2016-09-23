using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{
    float Distance = 81.5f;
    float OriginalSize;
    Vector3 TargetPosition;
    Vector3 OriginalPosition;
    bool follow = false;
    Camera cam;

    public float Speed = 10; 

    void Awake ()
    {
        TargetPosition = OriginalPosition = transform.position;
        OriginalSize = GetComponent<Camera>().orthographicSize;
        cam = GetComponent<Camera>();
    }

    void Update ()
    {
        transform.position += ((follow ? TargetPosition : OriginalPosition) - transform.position) * Time.deltaTime * Speed;
        cam.orthographicSize += ((follow ? OriginalSize * 0.5f: OriginalSize) - cam.orthographicSize) * Time.deltaTime * Speed;
    }

    public void LookingAt (Vector3 worldPos)
    {
        TargetPosition = worldPos - transform.forward * Distance;
        follow = true;
    }

    public void Return ()
    {
        follow = false;
    }
}
