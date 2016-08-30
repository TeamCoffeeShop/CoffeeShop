using UnityEngine;
using System.Collections;

public enum WaterMilkType
{
    None, HotWater, IcedWater, HotMilk, IcedMilk
}

public class WaterMilkInstantiator : MonoBehaviour
{
    public WaterMilkType WaterMilkType;

    public bool Ready = false;

    public float MaxAmount = 1000;
    public float CurrentAmount = 0;
    public float IncreaseAmount = 1;

    void OnMouseDrag()
    {
        switch(WaterMilkType)
        {
            case WaterMilkType.HotWater:
                CurrentAmount += IncreaseAmount;
                break;
            case WaterMilkType.IcedWater:
                CurrentAmount += IncreaseAmount;
                break;
            case WaterMilkType.HotMilk:
                CurrentAmount += IncreaseAmount;
                break;
            case WaterMilkType.IcedMilk:
                CurrentAmount += IncreaseAmount;
                break;
            default:
                break;

        }
    }
}
