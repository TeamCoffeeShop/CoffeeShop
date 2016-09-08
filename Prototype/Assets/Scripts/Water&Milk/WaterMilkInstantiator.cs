using UnityEngine;
using System.Collections;

public class WaterMilkInstantiator : MonoBehaviour
{
    public WaterMilkType WaterMilkType;
    public bool Ready = false;

    public float MaxAmount = 1000;
    public float CurrentAmount = 0;
    public float IncreaseAmount = 1;

    Minigame_CoffeeManager CM;
    Transform water;


    void Awake ()
    {
        CM = GameObject.Find("Manager").transform.Find("CoffeeManager").GetComponent<Minigame_CoffeeManager>();
    }

    void OnMouseDrag ()
    {
        switch(WaterMilkType)
        {
            case WaterMilkType.HotWater:
                CurrentAmount += IncreaseAmount;
                break;
            case WaterMilkType.IcedWater:
                CurrentAmount += IncreaseAmount;
                break;
            case WaterMilkType.HotMilk:
                CurrentAmount += IncreaseAmount;
                break;
            case WaterMilkType.IcedMilk:
                CurrentAmount += IncreaseAmount;
                break;
            default:
                break;
        }

        //water showoff
        if (!water)
            water = (GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Water"), new Vector3(5,2.216f,3.177f),Quaternion.identity) as GameObject).transform;
    }

    void OnMouseUp()
    {
        //save the amount to the cup
        if(CM.SelectedCoffee != null)
        {
            if(CurrentAmount <= MaxAmount)
            {
                CoffeeCupBehavior cup = CM.SelectedCoffee.GetComponent<CoffeeCupBehavior>();
                cup.WaterMilkType = WaterMilkType;
                cup.WaterMilkLevel = GetComponent<WaterMilkLevel>().Level;
            }
        }

        //finish water
        water.GetComponent<WaterFallingLogic>().filling = false;
        water = null;
    }
}
