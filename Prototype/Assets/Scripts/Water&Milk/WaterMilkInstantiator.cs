using UnityEngine;
using System.Collections;

public class WaterMilkInstantiator : MonoBehaviour
{
    public WaterMilkType WaterMilkType;
    public bool Ready = false;

    public float MaxAmount = 1000;
    public float CurrentAmount = 0;
    public float IncreaseAmount = 1;

    Transform water;

    void OnMouseDrag ()
    {
        switch(WaterMilkType)
        {
            case WaterMilkType.HotWater:
            case WaterMilkType.IcedWater:
            case WaterMilkType.HotMilk:
            case WaterMilkType.IcedMilk:
                //water showoff
                if (!water)
                    water = (GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Water"), new Vector3(5, 52.216f, 3.177f), Quaternion.identity) as GameObject).transform;
                CurrentAmount += IncreaseAmount;
                break;
            default:
                break;
        }
    }

    void OnMouseUp()
    {
        //save the amount to the cup
        if (MinigameManager.Get.CoffeeManager.SelectedCoffee != null)
        {
            if(CurrentAmount <= MaxAmount)
            {
                CoffeeCupBehavior cup = MinigameManager.Get.CoffeeManager.SelectedCoffee.GetComponent<CoffeeCupBehavior>();
                cup.WaterMilkType = WaterMilkType;
                cup.WaterMilkLevel = GetComponent<WaterMilkLevel>().Level;
            }
        }

        //finish water
        water.GetComponent<WaterFallingLogic>().filling = false;
        water = null;
    }
}
