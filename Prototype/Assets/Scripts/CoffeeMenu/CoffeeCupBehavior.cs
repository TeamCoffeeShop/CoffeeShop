using UnityEngine;
using System.Collections;

public class CoffeeCupBehavior : MonoBehaviour
{
    public CoffeeCupType CupType;
    public CoffeeDropType DropType;
    public WaterMilkType WaterMilkType;
    public OrderType DistinguishedMenuName;
    public float WaterMilkLevel;

    DragandDrop d;

    void Awake()
    {
        d = GetComponent<DragandDrop>();
    }

    void OnMouseDown()
    {
        //take it away from coffeemachine
        if (MinigameManager.Get.coffeeMachine.cup == gameObject)
        {
            if (MinigameManager.Get.coffeeMachine.TakeOutCoffeeCupFromMachine())
            {
                d.active = true;
            }
        }
    }

    void Update()
    {
        //machine
        if (d.inTarget == 1)
        {
            MinigameManager.Get.coffeeMachine.PutCoffeeCupToMachine(gameObject);
            d.active = false;
        }
    }
}