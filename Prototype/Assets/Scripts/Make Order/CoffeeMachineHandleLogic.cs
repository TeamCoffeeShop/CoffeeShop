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
            //move camera to next one
            Vector3 newPos = Camera.main.transform.position;
            newPos.x = MinigameManager.Get.coffeeMachine.transform.position.x;
            MinigameManager.Get.MakeOrderCamera.SetTargetLocation(newPos);
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
