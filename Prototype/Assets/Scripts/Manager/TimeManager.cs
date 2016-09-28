using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    void Update ()
    {
        InGameTime.Update();
    }
}

public static class InGameTime
{
    private static float TS = 1;
    private static float dT = 0;

    /// <summary>
    /// Do not call this on a normal occasion!
    /// This is only for updating dt, should not be called elsewhere.
    /// </summary>
    public static void Update ()
    {
        dT = Time.deltaTime * TS;
    }

    public static float timeScale
    {
        get
        {
            return TS;
        }
    }

    public static float deltaTime
    {
        get
        {
            return dT;
        }
    }

    public static void SetTimeScale(float speed)
    {
        TS = speed;
    }
}