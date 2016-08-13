using UnityEngine;
using System.Collections;

public class ResetManager : MonoBehaviour
{
    CameraLogic MainCamera;
    Vector3 StartPosition;

    // Use this for initialization
    void Start ()
    {
        MainCamera = GameObject.Find("Main Camera").GetComponent<CameraLogic>();
        StartPosition = new Vector3(-18f, 13f, -10f);
    }

    public void Reset()
    {
        //remove the coffee cup currently working on
        if (GameObject.FindGameObjectWithTag("CoffeeCup") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("CoffeeCup"));
        }
        //remove all the CoffeeBean objects in the level
        if (GameObject.FindGameObjectWithTag("CoffeeBean") != null)
        {
            GameObject[] names = GameObject.FindGameObjectsWithTag("CoffeeBean");
            foreach (GameObject item in names)
            {
                Destroy(item);
            }
        }
        //remove all the CoffeeDrop objects in the level
        if (GameObject.FindGameObjectWithTag("CoffeeDrop") != null)
        {
            GameObject[] names = GameObject.FindGameObjectsWithTag("CoffeeDrop");
            foreach (GameObject item in names)
            {
                Destroy(item);
            }
        }
        //remove all the CoffeePowder objects in the level
        if (GameObject.FindGameObjectWithTag("CoffeePowder") != null)
        {
            GameObject[] names = GameObject.FindGameObjectsWithTag("CoffeePowder");
            foreach (GameObject item in names)
            {
                Destroy(item);
            }
        }
        //remove all the Cream objects in the level
        if (GameObject.FindGameObjectWithTag("Cream") != null)
        {
            GameObject[] names = GameObject.FindGameObjectsWithTag("Cream");
            foreach (GameObject item in names)
            {
                Destroy(item);
            }
        }

        //reset status of water/milk
        //See WaterMilkInstantiator.cs and WaterMilkStatusChecker.cs for more details
        GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>().CurrentAmount = 0;
        GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>().HotWater = false;
        GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>().IcedWater = false;
        GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>().HotMilk = false;
        GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>().IcedMilk = false;
        GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>().Ready = false;

        //reset camera to original position
        MainCamera.TargetPosition = StartPosition;
    }
}
