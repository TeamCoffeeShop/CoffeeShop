using UnityEngine;
using System.Collections;

public class CoffeeMachineHandleLogic : MonoBehaviour
{
    DragandDrop d;
    public int CoffeeBeanType;

    public GameObject arrowForHandle;

    public bool mousedown;
    public int mousecheck;
    void Awake()
    {
        d = GetComponent<DragandDrop>();
        mousedown = false;
        mousecheck = 0;
    }

    void Update()
    {
        if (d.inTarget == 1)
        {
            //grinder
            if (CoffeeBeanType == 0)
            {
                MinigameManager.Get.handGrinder.PutCoffeeMachinHandleToGrinder(gameObject);
                arrowForHandle.SetActive(false);
                d.active = false;
            }
            //machine
            else
            {
                MinigameManager.Get.coffeeMachine.PutCoffeeMachinHandleToMachine(gameObject);
                //move camera to next one
                Vector3 newPos = Camera.main.transform.position;
                newPos.x = MinigameManager.Get.coffeeMachine.transform.position.x;
                MinigameManager.Get.MakeOrderCamera.SetTargetLocation(newPos);
                d.active = false;
            }
        }
    }

    void OnMouseDown ()
    {
        if (mousecheck == 0)
        {
            mousedown = true;
            ++mousecheck;
        }
        else
            mousedown = false;
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

    void OnMouseUp()
    {
        mousedown = false;
        mousecheck = 0;
    }
    public void AddPowderToHandle(int coffeeBeanType)
    {
        CoffeeBeanType = coffeeBeanType;
        d.active = true;
        GetComponent<OutlineHighlighter>().active = true;
        transform.GetChild(0).gameObject.SetActive(true);

        //set target to machine
        if(CoffeeBeanType != 0)
        {
            d.Target[0] = MinigameManager.Get.coffeeMachine.transform.FindChild("MachineCollider").gameObject;
            d.Highlight[0] = MinigameManager.Get.coffeeMachine.GetComponent<OutlineHighlighter>();
        }
    }

    public void DiscardPowderFromHandle()
    {
        CoffeeBeanType = 0;
        d.active = true;
        GetComponent<OutlineHighlighter>().active = true;
        transform.GetChild(0).gameObject.SetActive(false);

        d.Target[0] = MinigameManager.Get.handGrinder.transform.FindChild("Collider").gameObject;
        d.Highlight[0] = MinigameManager.Get.handGrinder.GetComponent<OutlineHighlighter>();
    }
}
