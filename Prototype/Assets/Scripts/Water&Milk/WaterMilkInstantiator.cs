using UnityEngine;
using System.Collections;

public class WaterMilkInstantiator : MonoBehaviour
{
    public bool HotWater = false;
    public bool IcedWater = false;
    public bool HotMilk = false;
    public bool IcedMilk = false;

    public bool Ready = false;

    public float MaxAmount = 1000;
    public float CurrentAmount = 0;
    public float IncreaseAmount = 1;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDrag()
    {
        if (HotWater == true)
        {
            CurrentAmount += IncreaseAmount;
        }

        if (IcedWater == true)
        {
            CurrentAmount += IncreaseAmount;
        }

        if (HotMilk == true)
        {
            CurrentAmount += IncreaseAmount;
        }

        if (IcedMilk == true)
        {
            CurrentAmount += IncreaseAmount;
        }

    }
}
