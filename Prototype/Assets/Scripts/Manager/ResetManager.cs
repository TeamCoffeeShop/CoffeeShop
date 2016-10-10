using UnityEngine;
using System.Collections;

public class ResetManager : MonoBehaviour
{
    public void Reset()
    {
        //empty coffeegrinder, machine, machine handle
        MinigameManager.Get.handGrinder.TakeOutCoffeeMachineHandleFromGrinder();
        MinigameManager.Get.handGrinder.DiscardCoffeeBean();
        MinigameManager.Get.coffeeMachine.TakeOutCoffeeCupFromMachine();
        MinigameManager.Get.coffeeMachine.TakeOutCoffeeMachineHandleFromMachine();
        MinigameManager.Get.coffeeMachineHandle.GetComponent<CoffeeMachineHandleLogic>().DiscardPowderFromHandle();

        //remove all the CoffeeBean objects in the level
        GameObject[] names = GameObject.FindGameObjectsWithTag("DragAndDrop");
        foreach (GameObject item in names)
        {
            if (item.name == "CoffeeMachineHandle")
                item.transform.position = new Vector3(-219.67f, 0.17f, 3.51f);
            else
                Destroy(item);
        }

        //remove all the coffeecups in the level
        //names = GameObject.FindGameObjectsWithTag("CoffeeCup");
        //foreach (GameObject item in names)
        //{
        //    if (item.GetComponent<CoffeeCupSelector>().active != 0)
        //        Destroy(item);
        //}

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
        //GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>().CurrentAmount = 0;
        //GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>().WaterMilkType = WaterMilkType.None;
        //GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>().Ready = false;

        //reset camera to original position
        MinigameManager.Get.MakeOrderCamera.Return();
        MinigameManager.Get.CoffeeManager.IsMakingOrderJustStarted = true;
    }
}
