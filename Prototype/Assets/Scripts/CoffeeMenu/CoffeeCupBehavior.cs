using UnityEngine;
using System.Collections;

public class CoffeeCupBehavior : MonoBehaviour
{
    public CoffeeCupType CupType;
    public CoffeeDropType DropType;
    public WaterMilkType WaterMilkType;
    public HotIceType HotIceType;
    public float WaterMilkLevel;
    public CoffeeCupSelector CoffeeCupShelfLink;

    DragandDrop d;
    bool BeginFinishing;

    void Awake()
    {
        d = GetComponent<DragandDrop>();
    }

    void Start()
    {
        d.Target[0] = MinigameManager.Get.coffeeMachine.transform.FindChild("MachineCollider").gameObject;
        d.Highlight[0] = MinigameManager.Get.coffeeMachine.GetComponent<OutlineHighlighter>();
        d.Target[1] = MinigameManager.Get.plate.transform.FindChild("Collider").gameObject;
        d.Highlight[1] = MinigameManager.Get.plate.GetComponent<OutlineHighlighter>();
    }

    void OnMouseDown()
    {
        //take it away from coffee shelf
        if (CoffeeCupShelfLink)
        {
            CoffeeCupShelfLink.CupInStock = null;
            CoffeeCupShelfLink = null;
        }

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
                if(MinigameManager.Get.coffeeMachine.PutCoffeeCupToMachine(gameObject))
                    d.active = false;
            }
            //Instantiator
            else
            {
                if(MinigameManager.Get.instantiator.PutCoffeeIntoInstantiator(this))
                    d.active = false;
            }
        }
        //Finish order
        else if (d.inTarget == 2)
        {
            BeginFinishing = true;
            d.active = false;
        }

        if(BeginFinishing)
        {
            Vector3 dP = d.Target[1].transform.position - transform.position;
            transform.position += dP * Time.deltaTime * d.MoveSpeed;

            if(dP.sqrMagnitude < 0.1f)
            {
                MinigameManager.Get.CoffeeManager.SaveFinishedOrder(this);
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