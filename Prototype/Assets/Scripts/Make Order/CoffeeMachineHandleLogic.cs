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
        }
    }
}
