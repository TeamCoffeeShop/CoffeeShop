using UnityEngine;
using System.Collections;

public enum CafeDecoType
{
    penetrable, impenetrable, seat
}

public class CafeDeco : MonoBehaviour
{
    public CafeDecoType   Type = CafeDecoType.penetrable;
    public         bool Filled = false;

    void Awake ()
    {
        MainGameManager.Get.Floor.Seats.Add(this);
    }

    void OnDestroy ()
    {
        MainGameManager.Get.Floor.Seats.Remove(this);
    }
}
