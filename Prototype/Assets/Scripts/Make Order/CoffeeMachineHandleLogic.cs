using UnityEngine;
using System.Collections;

public class CoffeeMachineHandleLogic : MonoBehaviour
{
    DragandDrop d;
    OutlineHighlighter h;
    public int CoffeeBeanType;

    void Awake()
    {
        d = GetComponent<DragandDrop>();
        h = GetComponent<OutlineHighlighter>();
    }

    void Update()
    {
        //grinder
        if (d.inTarget == 1)
        {
            MinigameManager.Get.handGrinder.PutCoffeeMachinHandleToGrinder(gameObject);
            d.active = false;
            h.active = false;
        }
        //machine
        else if (d.inTarget == 2)
        {
            MinigameManager.Get.coffeeMachine.PutCoffeeMachinHandleToMachine(gameObject);
            d.active = false;
            h.active = false;
        }
    }
}
