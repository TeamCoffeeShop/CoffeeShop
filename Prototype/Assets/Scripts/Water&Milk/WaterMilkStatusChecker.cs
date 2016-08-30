using UnityEngine;
using System.Collections;

public class WaterMilkStatusChecker : MonoBehaviour
{
    private WaterMilkInstantiator Instantiator;    

    // Use this for initialization
    void Start()
    {
        Instantiator = GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>();
    }

    void OnMouseDown()
    {
        //when the player clicks one of 4 water/milk while it's the first time,
        //then ready for instantiate the liquid

        if(!Instantiator.Ready)
        {
            if (gameObject.name == "HotWater")
            {
                Debug.Log("Hot Water Selected");
                Instantiator.WaterMilkType = WaterMilkType.HotWater;
                Instantiator.Ready = true;
            }

            else if (gameObject.name == "IcedWater")
            {
                Debug.Log("Iced Water Selected");
                Instantiator.WaterMilkType = WaterMilkType.IcedWater;
                Instantiator.Ready = true;
            }

            else if (gameObject.name == "HotMilk")
            {
                Debug.Log("Hot Milk Selected");
                Instantiator.WaterMilkType = WaterMilkType.HotMilk;
                Instantiator.Ready = true;
            }

            else if (gameObject.name == "IcedMilk")
            {
                Debug.Log("Iced Milk Selected");
                Instantiator.WaterMilkType = WaterMilkType.IcedMilk;
                Instantiator.Ready = true;
            }
        }
        else
        {
            Debug.Log("Already filled with liquid. Go to the first stage if you want to fill another");
        }
    }
}