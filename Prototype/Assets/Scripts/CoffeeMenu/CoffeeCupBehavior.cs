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
    OutlineHighlighter h;

    void Awake()
    {
        d = GetComponent<DragandDrop>();
        h = GetComponent<OutlineHighlighter>();
    }

    void Update()
    {
        //machine
        if (d.inTarget == 1)
        {
            MinigameManager.Get.coffeeMachine.PutCoffeeCupToMachine(gameObject);
            d.active = false;
            h.active = false;
        }
    }
}