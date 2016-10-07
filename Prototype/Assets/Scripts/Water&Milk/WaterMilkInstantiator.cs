using UnityEngine;
using System.Collections;

public class WaterMilkInstantiator : MonoBehaviour
{
    public HotIceType HotIceType;
    public WaterMilkType WaterMilkType;
    public bool MilkFoam = false;
    public bool Ready = false;

    public float MaxAmount = 1000;
    public float CurrentAmount = 0;
    public float IncreaseAmount = 1;

    //Ice
    private GameObject Ice;
    private int iceMax = 5;
    private int numOfIce = 0;

    //Steam
    private GameObject steam;

    Transform water;
    Transform milk;

    void OnMouseDrag ()
    {
        switch (HotIceType)
        {
            case HotIceType.Hot:
                if(!steam)
                {
                    steam = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/WhiteSmoke"));
                    steam.transform.position = transform.position;
                
                }
                break;
            case HotIceType.Ice:
                if (numOfIce < iceMax)
                {
                    Ice = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Ice"));
                    Ice.transform.position = transform.position;
                    numOfIce += 1;
                }
                break;
            default:
                break;
        }
        switch(WaterMilkType)
        {
            case WaterMilkType.Water:
                //water showoff
                if (!water)
                    water = (GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Water"), new Vector3(-189.88f, 2.216f, 3.177f), Quaternion.identity) as GameObject).transform;
                CurrentAmount += IncreaseAmount;
                break;
            case WaterMilkType.Milk:
                //water showoff
                if (!milk)
                    milk = (GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Milk"), new Vector3(-189.88f, 2.216f, 3.177f), Quaternion.identity) as GameObject).transform;
                CurrentAmount += IncreaseAmount;
                break;
            default:
                break;
        }
    }

    void OnMouseUp()
    {
        //save the amount to the cup
        //if (MinigameManager.Get.CoffeeManager.SelectedCoffee != null)
        //{
        //    if(CurrentAmount <= MaxAmount)
        //    {
        //        CoffeeCupBehavior cup = MinigameManager.Get.CoffeeManager.SelectedCoffee.GetComponent<CoffeeCupBehavior>();
        //        cup.WaterMilkType = WaterMilkType;
        //        cup.WaterMilkLevel = GetComponent<WaterMilkLevel>().Level;
        //    }
        //}

        //finish water
        if (WaterMilkType == WaterMilkType.Water)
        {
            water.GetComponent<WaterFallingLogic>().filling = false;
            water = null;
            milk.GetComponent<WaterFallingLogic>().filling = false;
        }
        else if(WaterMilkType == WaterMilkType.Milk)
        {
            milk.GetComponent<WaterFallingLogic>().filling = false;
            milk = null;
        }
    }
}
