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

    // Update is called once per frame
    void Update()
    {
            
    }

    void OnMouseDown()
    {
        //when the player clicks one of 4 water/milk while it's the first time,
        //then ready for instantiate the liquid

        if (gameObject.name == "HotWater" && Instantiator.Ready == false)
        {
            Debug.Log("Hot Water Selected");
            Instantiator.HotWater = true;
            Instantiator.IcedWater = false;
            Instantiator.HotMilk = false;
            Instantiator.IcedMilk = false;
            Instantiator.Ready = true;
        }

        else if(gameObject.name == "IcedWater" && Instantiator.Ready == false)
        {
            Debug.Log("Iced Water Selected");
            Instantiator.HotWater = false;
            Instantiator.IcedWater = true;
            Instantiator.HotMilk = false;
            Instantiator.IcedMilk = false;
            Instantiator.Ready = true;
        }

        else if(gameObject.name == "HotMilk" && Instantiator.Ready == false)
        {
            Debug.Log("Hot Milk Selected");
            Instantiator.HotWater = false;
            Instantiator.IcedWater = false;
            Instantiator.HotMilk = true;
            Instantiator.IcedMilk = false;
            Instantiator.Ready = true;
        }

        else if(gameObject.name == "IcedMilk" && Instantiator.Ready == false)
        {
            Debug.Log("Iced Milk Selected");
            Instantiator.HotWater = false;
            Instantiator.IcedWater = false;
            Instantiator.HotMilk = false;
            Instantiator.IcedMilk = true;
            Instantiator.Ready = true;
        }

        else if (Instantiator.Ready == true)
        {
            Debug.Log("Already filled with liquid. Go to the first stage if you want to fill another");
        }
    }
}