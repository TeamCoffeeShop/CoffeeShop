using UnityEngine;
using System.Collections;

public class Minigame_CoffeeManager : MonoBehaviour
{
    public GameObject SelectedCoffee;

    //save finished order
    public void SaveFinishedOrder()
    {
        GameObject orders = GameObject.Find("[[Finished Orders]]");

        if (orders)
        {
            Transform list = orders.transform;
            if (list && SelectedCoffee)
            {
                //check if coffee is finished
                if (LegitCoffee())
                {
                    Debug.Log("coffee successfully added to list!");
                    SelectedCoffee.transform.parent = list;
                    SelectedCoffee.SetActive(false);
                    SelectedCoffee.GetComponent<CoffeeCupBehavior>().DistinguishedMenuName = CoffeeOrderSetup.DistinguishCreatedMenu(ref SelectedCoffee);
                    SelectedCoffee = null;
                }

            }
        }
    }

    //check if the coffee is legit
    public bool LegitCoffee()
    {
        CoffeeCupBehavior cup = SelectedCoffee.GetComponent<CoffeeCupBehavior>();

        if (cup.DropType == CoffeeDropType.None)
            return false;

        return true;
    }
}
