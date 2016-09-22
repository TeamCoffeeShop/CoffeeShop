using UnityEngine;
using System.Collections;

public enum CafeDecoType
{
    penetrable, impenetrable, seat
}

public class CafeDeco : MonoBehaviour
{
    public CafeDecoType     Type = CafeDecoType.penetrable;
    public         bool   Filled = false;
    public        float    Float;
    public         bool Selected = false;
    float OriginalFloat;

    void Start ()
    {
        MainGameManager.Get.Floor.Seats.Add(this);
        Float = OriginalFloat = transform.localPosition.y;
        Float += 3;
    }

    void Update ()
    {
        const float Speed = 10;

        if (Selected)
            transform.localPosition += new Vector3(0, (Float - transform.localPosition.y) * Time.deltaTime * Speed, 0);
        else
            transform.localPosition += new Vector3(0, (OriginalFloat - transform.localPosition.y) * Time.deltaTime * Speed, 0);
    }

    void OnDestroy ()
    {
        MainGameManager.Get.Floor.Seats.Remove(this);
    }

    public void Rotate ()
    {
        transform.Rotate(0,90,0);
    }
}
