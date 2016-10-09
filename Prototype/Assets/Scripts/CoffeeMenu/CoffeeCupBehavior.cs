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
        else if (MinigameManager.Get.instantiator.cup == this)
        {
            if(MinigameManager.Get.instantiator.TakeOutCoffeeCupFromInstantiator())
                d.active = true;
        }
    }

    void Update()
    {
        if (d.inTarget == 1)
        {
            //machine
            if(DropType == CoffeeDropType.None)
            {
                MinigameManager.Get.coffeeMachine.PutCoffeeCupToMachine(gameObject);
                d.active = false;
            }
            //Instantiator
            else
            {
                MinigameManager.Get.instantiator.PutCoffeeIntoInstantiator(this);
                d.active = false;
            }
        }
    }

    public void PutCoffeeDropIntoCup (int powdertype)
    {
        switch (powdertype)
        {
            case 1:
                DropType = CoffeeDropType.CoffeeDrop1;
                break;
            case 2:
                DropType = CoffeeDropType.CoffeeDrop2;
                break;
            default:
                break;

        }

        if (DropType != CoffeeDropType.None)
        {
            if (transform.childCount != 0)
                transform.GetChild(0).gameObject.SetActive(true);

            //change the target to instantiator

            d.Highlight[0] = MinigameManager.Get.instantiator.GetComponent<OutlineHighlighter>();
            d.Target[0] = d.Highlight[0].transform.FindChild("Collider").gameObject;
        }
    }
}