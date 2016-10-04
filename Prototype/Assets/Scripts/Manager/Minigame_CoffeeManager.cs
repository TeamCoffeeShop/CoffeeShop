using UnityEngine;
using System.Collections;

public class Minigame_CoffeeManager : MonoBehaviour
{
    public GameObject SelectedCoffee;

    //save finished order
    public void SaveFinishedOrder()
    {
            Transform list = MainGameManager.Get.Canvas_OrderHUD.transform;
            if (list && SelectedCoffee)
            {
                //check if coffee is finished
                if (LegitCoffee())
                {
                    SelectedCoffee.transform.parent = list.FindChild("Finished Orders");
                    SelectedCoffee.SetActive(false);
                    SelectedCoffee.GetComponent<CoffeeCupBehavior>().DistinguishedMenuName = CoffeeOrderSetup.DistinguishCreatedMenu(ref SelectedCoffee);
                    SelectedCoffee = null;
                }

            }
    }

    //check if the coffee is legit
    public bool LegitCoffee()
    {
        CoffeeCupBehavior cup = SelectedCoffee.GetComponent<CoffeeCupBehavior>();

        if (cup.DropType == CoffeeDropType.None && cup.WaterMilkType == WaterMilkType.None)
            return false;

        return true;
    }

    void Update ()
    {

    }
}
