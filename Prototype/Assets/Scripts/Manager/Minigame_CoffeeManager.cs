using UnityEngine;
using System.Collections;

public class Minigame_CoffeeManager : MonoBehaviour
{
    public float LockSpeed = 10;
    public bool IsMakingOrderJustStarted = true;

    //save finished order
    public void SaveFinishedOrder(CoffeeCupBehavior cup)
    {
        //check if coffee is finished
        if (LegitCoffee(cup))
            MainGameManager.Get.Canvas_OrderHUD.CreateOrderInUI(cup.transform.position, CoffeeOrderSetup.DistinguishCreatedMenu(cup));

        //delete cup
        DestroyObject(cup.gameObject);
    }

    //check if the coffee is legit
    public bool LegitCoffee(CoffeeCupBehavior cup)
    {
        if (cup.DropType == CoffeeDropType.None && cup.WaterMilkType == WaterMilkType.None)
            return false;

        return true;
    }
}
