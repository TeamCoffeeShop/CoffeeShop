using UnityEngine;
using System.Collections;

public class CoffeeMachineHandleLogic : MonoBehaviour
{
    DragandDrop d;
    public int CoffeeBeanType;

    void Awake()
    {
        d = GetComponent<DragandDrop>();
    }

    void Update()
    {
        //grinder
        if (d.inTarget == 1)
        {
            MinigameManager.Get.handGrinder.PutCoffeeMachinHandleToGrinder(gameObject);
            d.active = false;
        }
        //machine
        else if (d.inTarget == 2)
        {
            MinigameManager.Get.coffeeMachine.PutCoffeeMachinHandleToMachine(gameObject);
            d.active = false;
        }
    }

    void OnMouseDown ()
    {
        //take it away from coffeegrinder
        if (MinigameManager.Get.handGrinder.MachineHandle == gameObject)
        {
            MinigameManager.Get.handGrinder.TakeOutCoffeeMachineHandleFromGrinder();
            d.active = true;
        }

        //take it away from coffeemachine
        if (MinigameManager.Get.coffeeMachine.handle == gameObject)
        {
            if(MinigameManager.Get.coffeeMachine.TakeOutCoffeeMachineHandleFromMachine())
            {
                d.active = true;
            }
        }
    }
}
