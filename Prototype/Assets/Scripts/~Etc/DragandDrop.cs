using UnityEngine;
using System.Collections;

public class DragandDrop : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    Vector2 Xbound = new Vector2(-225, -177);
    Vector2 Ybound = new Vector2(0.5f, 6);
    Vector2 Zbound = new Vector2(3, 3);

    bool Grab = false;
    bool InGrinder = false;
    bool InHandle = false;
    bool InMachine = false;
    bool InCup = false;

    public enum ObjectType
    {
        none, coffeeBean, coffeePowder, coffeeMachineHandle
    }
    public ObjectType type;
    public int typeIndex;

    //variables to access handgrinder
    GameObject handGrinder;
    HandGrinderScript hGrinderScript;
    CoffeeDrop cMachineScript;

    //variable to access coffee machine handle
    GameObject machineHandle;

    //variable to access coffee handle
    GameObject EspressoPowder;
    GameObject coffeepowderInHandle;

    //variable to access coffeemachine
    GameObject coffeeMachine;

    void Awake()
    {
        handGrinder = GameObject.Find("HandGrinder");
        coffeeMachine = GameObject.Find("CoffeeMachine");
        machineHandle = GameObject.Find("CoffeeMachineHandle");
        EspressoPowder = Resources.Load<GameObject>("Prefab/EspressoPowder");

        hGrinderScript = handGrinder.GetComponent<HandGrinderScript>();
        cMachineScript = coffeeMachine.GetComponent<CoffeeDrop>();
    }

    void OnMouseDown()
    {
        //grab check
        Grab = true;
        //cursor invisible
        Cursor.visible = false;

        //for drag and drop
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
         screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
         offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  

        //highlighter
        switch (type)
        {
            case ObjectType.coffeeBean:
                {
                    handGrinder.GetComponent<HandGrinderScript>().highlightWhenAble = true;
                }
                break;
        }

        if(gameObject == machineHandle)
            InMachine = true;
    }

    void OnMouseDrag()
    {
        //for drag and drop
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

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

        // Z bound
        if (curPosition.z < Zbound.x)
            curPosition.z = Zbound.x;
        else if (curPosition.z > Zbound.y)
            curPosition.z = Zbound.y;

        transform.position = curPosition;
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    void OnMouseUp()
    {
        //grab check
        Grab = false;   
        //cursor visible
        Cursor.visible = true;

        switch (type)
        {
            case ObjectType.coffeeBean:
                //when the player drags and drops the coffee bean into the grinder, destroy the coffee bean object
                if (InGrinder == true)
                {
                    //when the list is empty
                    if (hGrinderScript.CoffeeBeans.Count == 0)
                    {
                        //put first coffee bean info
                        hGrinderScript.CoffeeBeans.Add(new CoffeeBean(true, typeIndex));
                        //and then destroy the coffee bean object
                        Destroy(gameObject);
                        //Rotate Camera
                        //Camera.main.GetComponent<CameraLogic>().PreviousPosition = Camera.main.GetComponent<CameraLogic>().TargetPosition;
                        //Camera.main.GetComponent<CameraLogic>().TargetPosition = new Vector3(-6, 60, 5);
                        //Camera.main.transform.Rotate(90, 0, 0);
                        //Set grinder status to start status
                        hGrinderScript.rotationImage.enabled = true;
                        hGrinderScript.coffeeBeanCheck = true;
                        hGrinderScript.totalRotation = 0;
                    }
                }
                else
                    handGrinder.GetComponent<HandGrinderScript>().highlightWhenAble = false;
                break;

            case ObjectType.coffeePowder:
                //when the player drags and drops the coffee powder into the the handle, destory the coffee powder object
                if (InHandle == true)
                {
                    //when the list is empty
                    if (cMachineScript.CoffeePowders.Count == 0)
                    {
                        int content = hGrinderScript.PowderContent;
                        //put first coffee powder info
                        cMachineScript.CoffeePowders.Add(new CoffeePowder(true, typeIndex, content));
                        //and then destroy the coffee powder object
                        Destroy(gameObject);
                        // Add espresson powder in the handle
                        coffeepowderInHandle = (GameObject)Instantiate(EspressoPowder, machineHandle.transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
                        coffeepowderInHandle.transform.parent = machineHandle.transform;
                    }
                }
                break;
            case ObjectType.coffeeMachineHandle:
                //when the player drags and drops the coffee machine handle into the coffee machine, destory the coffee powder object
                if (InMachine == true)
                {
                    Vector3 coffeemachinescreen = Camera.main.WorldToScreenPoint(coffeeMachine.transform.position);
                    Vector3 machineoffset = coffeeMachine.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, coffeemachinescreen.z));

                    if (coffeemachinescreen.x - 30 < Input.mousePosition.x && coffeemachinescreen.x + 30 > Input.mousePosition.x)
                    {
                        if (cMachineScript.CoffeePowders.Count != 0)
                        {
                            if (coffeepowderInHandle)
                                Destroy(coffeepowderInHandle);

                            Destroy(machineHandle);
                            // destroy the coffee machine handle object
                            Destroy(gameObject);
                            coffeeMachine.GetComponent<CoffeeDrop>().CameraRotate = true;
                        }
                    }
                }
                break;
        }
        
        //when the player drags and drops the coffee drop into the coffee cup, destory the coffee drop object
        if (InCup == true)
        {
            if(MinigameManager.Get.CoffeeManager.SelectedCoffee != null)
                if (gameObject.tag == "CoffeeDrop")
                {
                    switch(gameObject.name)
                    {
                        case "CoffeeDrop1":
                            MinigameManager.Get.CoffeeManager.SelectedCoffee.GetComponent<CoffeeCupBehavior>().DropType = CoffeeDropType.CoffeeDrop1;
                            CoffeeBehaviourSetup.SetCoffee(ref MinigameManager.Get.CoffeeManager.SelectedCoffee);
                            Destroy(gameObject);
                            break;
                        case "CoffeeDrop2":
                            MinigameManager.Get.CoffeeManager.SelectedCoffee.GetComponent<CoffeeCupBehavior>().DropType = CoffeeDropType.CoffeeDrop2;
                            CoffeeBehaviourSetup.SetCoffee(ref MinigameManager.Get.CoffeeManager.SelectedCoffee);
                            Destroy(gameObject);
                            break;
                        default:
                            break;
                    }
                }
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if(Grab)
            switch (type)
            {
                case ObjectType.coffeeBean:
                    //and drops it into the handgrinder
                    if (col.gameObject == handGrinder)
                    {
                        InGrinder = true;
                    }
                    break;

                case ObjectType.coffeePowder:
                    //if the player picks up the coffeepowder
                    if (col.gameObject == machineHandle)
                    {
                        InHandle = true;
                    }
                    break;
                case ObjectType.coffeeMachineHandle:
                    //if the player picks up the coffee machine handle
                    if (col.gameObject == coffeeMachine)
                    {
                        InMachine = true;
                    }
                    break;
            }

        //if the player picks up the coffeedrop
        if (gameObject.tag == "CoffeeDrop" && this.Grab == true)
        {
            if (MinigameManager.Get.CoffeeManager.SelectedCoffee != null)
                if (col.gameObject == MinigameManager.Get.CoffeeManager.SelectedCoffee)
                {
                    InCup = true;
                }
        }
    }

    void OnCollisionExit(Collision col)
    {
        if(Grab)
            switch (type)
            {
                case ObjectType.coffeeBean:
                    //and drops it into the handgrinder
                    if (col.gameObject == handGrinder)
                    {
                        InGrinder = false;
                    }
                    break;

                case ObjectType.coffeePowder:
                    if (col.gameObject == machineHandle)
                    {
                        InHandle = false;
                    }
                    break;
                case ObjectType.coffeeMachineHandle:
                    if (col.gameObject == coffeeMachine)
                    {
                        InMachine = false;
                    }
                    break;
            }

        if (gameObject.tag == "CoffeeDrop")
        {
            if (MinigameManager.Get.CoffeeManager.SelectedCoffee != null)
                if (col.gameObject == MinigameManager.Get.CoffeeManager.SelectedCoffee && this.Grab == true)
                {
                    InCup = false;
                }
        }
    }
}
