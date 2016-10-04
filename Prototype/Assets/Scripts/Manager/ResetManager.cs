using UnityEngine;
using System.Collections;

public class ResetManager : MonoBehaviour
{
    public Vector3 OriginalPosition;

    public void Reset()
    {
        //remove the coffee cup currently working on
        if (MinigameManager.Get.CoffeeManager.SelectedCoffee != null)
        {
            Destroy(MinigameManager.Get.CoffeeManager.SelectedCoffee);
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
        GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>().WaterMilkType = WaterMilkType.None;
        GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>().Ready = false;

        //reset camera to original position
        //MinigameManager.Get.MakeOrderCamera.TargetPosition = StartPosition;
    }
}
