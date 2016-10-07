using UnityEngine;
using System.Collections;

public class Minigame_CoffeeManager : MonoBehaviour
{
    public float LockSpeed = 10;
    public bool IsMakingOrderJustStarted = true;

    //save finished order
    public void SaveFinishedOrder()
    {
            Transform orderHUD = MainGameManager.Get.Canvas_OrderHUD.transform;
            GameObject[] cups = GameObject.FindGameObjectsWithTag("CoffeeCup");
            if (orderHUD)
            {
                foreach (GameObject cup in cups)
                {
                    //check if coffee is finished
                    if (LegitCoffee(cup.GetComponent<CoffeeCupBehavior>()))
                    {
                        cup.transform.parent = orderHUD.FindChild("Finished Orders");
                        cup.SetActive(false);
                        cup.GetComponent<CoffeeCupBehavior>().DistinguishedMenuName = CoffeeOrderSetup.DistinguishCreatedMenu(cup);
                    }
                }
            }
    }

    //check if the coffee is legit
    public bool LegitCoffee(CoffeeCupBehavior cup)
    {
        if (cup.DropType == CoffeeDropType.None && cup.WaterMilkType == WaterMilkType.None)
            return false;

        return true;
    }
}
