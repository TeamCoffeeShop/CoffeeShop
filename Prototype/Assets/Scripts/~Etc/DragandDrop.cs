using UnityEngine;
using System.Collections;

public class DragandDrop : MonoBehaviour
{
    public bool active = true;
    public Vector2 Xbound = new Vector2(-225, -177);
    public Vector2 Ybound = new Vector2(0.5f, 6);
    public GameObject[] Target;

    private bool pActive;
    private int InTarget_Return = 0;
    private int InTarget = 0;
    private bool Grab = false;

    public int inTarget
    {
        get
        {
            return InTarget_Return;
        }
    }

    void OnMouseDown ()
    {
        Grab = true;

        if (active)
            //highlight on targets
            foreach (GameObject target in Target)
            {
                OutlineHighlighter h = target.GetComponent<OutlineHighlighter>();

                if (h != null)
                    h.active = true;
            }
    }

    void OnMouseDrag ()
    {
        //moving object according to mouse coordinate
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane p = new Plane(new Vector3(0, 0, -1), 3.51f);
        float d;
        p.Raycast(ray, out d);
        Vector3 curPosition = Camera.main.transform.position + ray.direction * d;

        //X bound
        if (curPosition.x < Xbound.x)
            curPosition.x = Xbound.x;
        else if (curPosition.x > Xbound.y)
            curPosition.x = Xbound.y;

        //Y bound
        if (curPosition.y < Ybound.x)
            curPosition.y = Ybound.x;
        else if (curPosition.y > Ybound.y)
            curPosition.y = Ybound.y;

        transform.position = curPosition;
    }

    void OnMouseUp()
    {
        Grab = false;

        if (InTarget != 0)
        {
            InTarget_Return = InTarget;
        }

        if(active)
            //highlight off targets
            foreach (GameObject target in Target)
            {
                OutlineHighlighter h = target.GetComponent<OutlineHighlighter>();

                if (h != null)
                    h.active = false;
            }
    }

    void OnCollisionEnter(Collision col)
    {
        int i = 1;
        if(Grab)
            foreach (GameObject obj in Target)
            {
                if(obj == col.gameObject)
                {
                    InTarget = i;
                    break;
                }
                ++i;
            }
    }

    void OnCollisionExit(Collision col)
    {
        if (Grab)
            foreach (GameObject obj in Target)
            {
                if (obj == col.gameObject)
                {
                    InTarget = 0;
                    InTarget_Return = 0;
                    break;
                }
            }
    }

    void Start ()
    {
        pActive = !active;
    }

    void Update ()
    {
        if (pActive != active)
        {
            pActive = active;

            InTarget = 0;
            InTarget_Return = 0;
            Grab = false;
        }
    }

    //void OnMouseUp()
    //{
    //    //grab check
    //    Grab = false;   

    //    if (InTarget)
    //        switch (type)
    //        {
    //            case ObjectType.coffeeBean:
    //                //when the player drags and drops the coffee bean into the grinder, destroy the coffee bean object                        
    //                if (MinigameManager.Get.handGrinder.CoffeeBeans.Count == 0)
    //                {
    //                    //put first coffee bean info
    //                    MinigameManager.Get.handGrinder.CoffeeBeans.Add(new CoffeeBean(true, typeIndex));
    //                    //and then destroy the coffee bean object
    //                    Destroy(gameObject);
    //                    //Rotate Camera
    //                    //Camera.main.GetComponent<CameraLogic>().PreviousPosition = Camera.main.GetComponent<CameraLogic>().TargetPosition;
    //                    //Camera.main.GetComponent<CameraLogic>().TargetPosition = new Vector3(-6, 60, 5);
    //                    //Camera.main.transform.Rotate(90, 0, 0);
    //                    //Set grinder status to start status
    //                    MinigameManager.Get.handGrinder.rotationImage.enabled = true;
    //                    MinigameManager.Get.handGrinder.coffeeBeanCheck = true;
    //                    MinigameManager.Get.handGrinder.totalRotation = 0;
    //                }
    //                MinigameManager.Get.handGrinder.highlightWhenAble = false;
    //                break;

    //            //case ObjectType.coffeePowder:
    //            //    //when the player drags and drops the coffee powder into the the handle, destory the coffee powder object
    //            //    if (MinigameManager.Get.coffeeMachine.CoffeePowders.Count == 0)
    //            //    {
    //            //        int content = MinigameManager.Get.handGrinder.PowderContent;
    //            //        //put first coffee powder info
    //            //        MinigameManager.Get.coffeeMachine.CoffeePowders.Add(new CoffeePowder(true, typeIndex, content));
    //            //        //and then destroy the coffee powder object
    //            //        Destroy(gameObject);
    //            //        // Add espresson powder in the handle
    //            //        coffeepowderInHandle = (GameObject)Instantiate(Resources.Load<GameObject>("Prefab/EspressoPowder"), MinigameManager.Get.coffeeMachineHandle.transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
    //            //        coffeepowderInHandle.transform.parent = MinigameManager.Get.coffeeMachineHandle.transform;
    //            //    }
    //            //    break;
    //            case ObjectType.coffeeMachineHandle:
    //                //when the player drags and drops the coffee machine handle into the coffee machine, destory the coffee powder object
    //                Vector3 coffeemachinescreen = Camera.main.WorldToScreenPoint(MinigameManager.Get.coffeeMachine.transform.position);
    //                Vector3 machineoffset = MinigameManager.Get.coffeeMachine.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, coffeemachinescreen.z));

    //                if (coffeemachinescreen.x - 30 < Input.mousePosition.x && coffeemachinescreen.x + 30 > Input.mousePosition.x)
    //                {
    //                    if (MinigameManager.Get.coffeeMachine.CoffeePowders.Count != 0)
    //                    {
    //                        if (coffeepowderInHandle)
    //                            Destroy(coffeepowderInHandle);

    //                        Destroy(MinigameManager.Get.coffeeMachineHandle);
    //                        // destroy the coffee machine handle object
    //                        Destroy(gameObject);
    //                        MinigameManager.Get.coffeeMachine.CameraRotate = true;
    //                    }
    //                }

    //                MinigameManager.Get.handGrinder.highlightWhenAble = false;
    //                break;
    //        }
        
    //    //when the player drags and drops the coffee drop into the coffee cup, destory the coffee drop object
    //    //if (InTarget == true)
    //    //{
    //    //    if(MinigameManager.Get.CoffeeManager.SelectedCoffee != null)
    //    //        if (gameObject.tag == "CoffeeDrop")
    //    //        {
    //    //            switch(gameObject.name)
    //    //            {
    //    //                case "CoffeeDrop1":
    //    //                    MinigameManager.Get.CoffeeManager.SelectedCoffee.GetComponent<CoffeeCupBehavior>().DropType = CoffeeDropType.CoffeeDrop1;
    //    //                    CoffeeBehaviourSetup.SetCoffee(ref MinigameManager.Get.CoffeeManager.SelectedCoffee);
    //    //                    Destroy(gameObject);
    //    //                    break;
    //    //                case "CoffeeDrop2":
    //    //                    MinigameManager.Get.CoffeeManager.SelectedCoffee.GetComponent<CoffeeCupBehavior>().DropType = CoffeeDropType.CoffeeDrop2;
    //    //                    CoffeeBehaviourSetup.SetCoffee(ref MinigameManager.Get.CoffeeManager.SelectedCoffee);
    //    //                    Destroy(gameObject);
    //    //                    break;
    //    //                default:
    //    //                    break;
    //    //            }
    //    //        }
    //    //}
    //}

    //void Update ()
    //{
    //    //switch (type)
    //    //{
    //    //    case ObjectType.coffeeMachineHandle:
    //    //        if (MinigameManager.Get.CoffeeManager.step == 2)
    //    //            h.active = true;
    //    //        else
    //    //            h.active = false;
    //    //        break;
    //    //}
    //}

    //void OnCollisionEnter(Collision col)
    //{
    //    if (Grab && col.gameObject == Target)
    //        InTarget = true;

    //    //if the player picks up the coffeedrop
    //    //if (gameObject.tag == "CoffeeDrop" && this.Grab == true)
    //    //{
    //    //    if (MinigameManager.Get.CoffeeManager.SelectedCoffee != null)
    //    //        if (col.gameObject == MinigameManager.Get.CoffeeManager.SelectedCoffee)
    //    //        {
    //    //            InTarget = true;
    //    //        }
    //    //}
    //}

    //void OnCollisionExit(Collision col)
    //{
    //    if (Grab && col.gameObject == Target)
    //        InTarget = false;

    ////    if (gameObject.tag == "CoffeeDrop")
    ////    {
    ////        if (MinigameManager.Get.CoffeeManager.SelectedCoffee != null)
    ////            if (col.gameObject == MinigameManager.Get.CoffeeManager.SelectedCoffee && this.Grab == true)
    ////            {
    ////                InTarget = false;
    ////            }
    ////    }
    //}
}
