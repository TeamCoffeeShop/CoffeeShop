using UnityEngine;
using System.Collections;

public class Minigame_CoffeeManager : MonoBehaviour
{
    public GameObject SelectedCoffee;
    public bool LockToCamera;
    public float LockSpeed = 10;
    
    
    //step 0 : selecting cup
    //step 1 : selecting coffee bean
    //step 2 : grind the coffee bean
    public int step = 0;



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
        //move selected coffee to infront of camera
        if (SelectedCoffee != null && LockToCamera)
        {
            Vector3 TargetPos = MinigameManager.Get.MakeOrderCamera.transform.position;
            TargetPos += MinigameManager.Get.MakeOrderCamera.transform.forward * 5;
            TargetPos -= MinigameManager.Get.MakeOrderCamera.transform.up * 2;

            SelectedCoffee.transform.position += (TargetPos - SelectedCoffee.transform.position) * Time.deltaTime * LockSpeed;
        }
    }
}
