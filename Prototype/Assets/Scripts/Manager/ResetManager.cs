using UnityEngine;
using System.Collections;

public class ResetManager : MonoBehaviour
{
    public void Reset()
    {
        //remove the coffee cup currently working on
        if (MinigameManager.Get.CoffeeManager.SelectedCoffee != null)
        {
            Destroy(MinigameManager.Get.CoffeeManager.SelectedCoffee);
        }

        //remove all the CoffeeBean objects in the level
        GameObject[] names = GameObject.FindGameObjectsWithTag("DragAndDrop");
        foreach (GameObject item in names)
        {
            if (item.name == "CoffeeMachineHandle")
                item.transform.position = new Vector3(-214.46f, 0.17f, 3.51f);
            else
                Destroy(item);
        }

        ////remove all the CoffeeDrop objects in the level
        //if (GameObject.FindGameObjectWithTag("CoffeeDrop") != null)
        //{
        //    names = GameObject.FindGameObjectsWithTag("CoffeeDrop");
        //    foreach (GameObject item in names)
        //    {
        //        Destroy(item);
        //    }
        //}
        ////remove all the Cream objects in the level
        //if (GameObject.FindGameObjectWithTag("Cream") != null)
        //{
        //    names = GameObject.FindGameObjectsWithTag("Cream");
        //    foreach (GameObject item in names)
        //    {
        //        Destroy(item);
        //    }
        //}

        //reset status of water/milk
        //See WaterMilkInstantiator.cs and WaterMilkStatusChecker.cs for more details
        GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>().CurrentAmount = 0;
        GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>().WaterMilkType = WaterMilkType.None;
        GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>().Ready = false;

        //reset camera to original position
        MinigameManager.Get.MakeOrderCamera.Return();
        MinigameManager.Get.CoffeeManager.IsMakingOrderJustStarted = true;
    }
}
