using UnityEngine;
using System.Collections;

public class DragandDrop : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;

    float DragHeight = 51;

    bool Grab = false;
    bool InGrinder = false;
    bool InHandle = false;
    bool InMachine = false;
    bool InCup = false;

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

    //variable to access coffeeCup
    GameObject coffeeCup;

    void Awake()
    {
        handGrinder = GameObject.Find("HandGrinder");
        coffeeMachine = GameObject.Find("CoffeeMachine");
        machineHandle = GameObject.Find("CoffeeMachineHandle");
        EspressoPowder = Resources.Load<GameObject>("Prefab/EspressoPowder");

        hGrinderScript = handGrinder.GetComponent<HandGrinderScript>();
        cMachineScript = coffeeMachine.GetComponent<CoffeeDrop>();
    }

    void FixedUpdate()
    {
    }

    void Update()
    {
        if (coffeeCup == null)
            coffeeCup = MinigameManager.Get.CoffeeManager.SelectedCoffee;
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

        if(gameObject == machineHandle)
            InMachine = true;
    }

    void OnMouseDrag()
    {
        //for drag and drop
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
         Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
         Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
         curPosition.y = DragHeight;
         transform.position = curPosition;
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    void OnMouseUp()
    {
        //grab check
        Grab = false;   
        //cursor visible
        Cursor.visible = true;

        //when the player drags and drops the coffee bean into the grinder, destroy the coffee bean object
        if (InGrinder == true)
        {
            //for first coffee bean type
            if (gameObject.name == "CoffeeBean1(Clone)")
            {
                //when the list is empty
                if (hGrinderScript.CoffeeBeans.Count == 0)
                {
                    //put first coffee bean info
                    hGrinderScript.CoffeeBeans.Add(new CoffeeBean(true, 1));
                    //and then destroy the coffee bean object
                    Destroy(gameObject);
                    //Rotate Camera
                    Camera.main.GetComponent<CameraLogic>().PreviousPosition = Camera.main.GetComponent<CameraLogic>().TargetPosition;
                    Camera.main.GetComponent<CameraLogic>().TargetPosition = new Vector3(-6, 60, 5);
                    Camera.main.transform.Rotate(90, 0, 0);
                    //Set grinder status to start status
                    hGrinderScript.rotationImage.enabled = true;
                    hGrinderScript.coffeeBeanCheck = true;
                    hGrinderScript.totalRotation = 0;
                }
            }

            //for second coffee bean type
            if (gameObject.name == "CoffeeBean2(Clone)")
            {
                //when the list is empty
                if (hGrinderScript.CoffeeBeans.Count == 0)
                {
                    //put second coffee bean info
                    hGrinderScript.CoffeeBeans.Add(new CoffeeBean(true, 2));
                    //and then destroy the coffee bean object
                    Destroy(gameObject);
                    //Rotate Camera
                    Camera.main.GetComponent<CameraLogic>().PreviousPosition = Camera.main.GetComponent<CameraLogic>().TargetPosition;
                    Camera.main.GetComponent<CameraLogic>().TargetPosition = new Vector3(-6, 60, 5);
                    Camera.main.transform.Rotate(90, 0, 0);
                    //Set grinder status to start status
                    hGrinderScript.rotationImage.enabled = true;
                    hGrinderScript.coffeeBeanCheck = true;
                    hGrinderScript.totalRotation = 0;
                }
            }
        }

        //when the player drags and drops the coffee powder into the the handle, destory the coffee powder object
        if (InHandle == true)
        {
            //for first coffee powder type
            if (gameObject.name == "CoffeePowder1")
            {
                //when the list is empty
                if (cMachineScript.CoffeePowders.Count == 0)
                {
                    int content = hGrinderScript.PowderContent;
                    //put first coffee powder info
                    cMachineScript.CoffeePowders.Add(new CoffeePowder(true, 1, content));
                    //and then destroy the coffee powder object
                    Destroy(gameObject);
                    // Add espresson powder in the handle
                    coffeepowderInHandle = (GameObject)Instantiate(EspressoPowder, machineHandle.transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
                    coffeepowderInHandle.transform.parent = machineHandle.transform;
                }
            }

            //for first coffee powder type
            if (gameObject.name == "CoffeePowder2")
            {
                //when the list is empty
                if (cMachineScript.CoffeePowders.Count == 0)
                {
                    int content = hGrinderScript.PowderContent;
                    //put first coffee powder info
                    cMachineScript.CoffeePowders.Add(new CoffeePowder(true, 2, content));
                    //and then destroy the coffee powder object
                    Destroy(gameObject);
                    // Add espresson powder in the handle
                    coffeepowderInHandle = (GameObject)Instantiate(EspressoPowder, machineHandle.transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);

                }
            }
        }

        //when the player drags and drops the coffee machine handle into the coffee machine, destory the coffee powder object
        if (InMachine == true)
        {
            if (gameObject.name == "CoffeeMachineHandle")
            {
                Vector3 coffeemachinescreen = Camera.main.WorldToScreenPoint(coffeeMachine.transform.position);
                Vector3 machineoffset = coffeeMachine.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, coffeemachinescreen.z));

                if (coffeemachinescreen.x - 30 < Input.mousePosition.x && coffeemachinescreen.x + 30 > Input.mousePosition.x)
                   // && coffeemachinescreen.y - 30 < Input.mousePosition.y && coffeemachinescreen.y + 30> Input.mousePosition.y)
                    //&& coffeemachinescreen.z - 30 < Input.mousePosition.z && coffeemachinescreen.z + 30 > Input.mousePosition.z)
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
        }

        //when the player drags and drops the coffee drop into the coffee cup, destory the coffee drop object
        if (InCup == true)
        {
            if (gameObject.tag == "CoffeeDrop")
            {
                switch(gameObject.name)
                {
                    case "CoffeeDrop1":
                        coffeeCup.GetComponent<CoffeeCupBehavior>().DropType = CoffeeDropType.CoffeeDrop1;
                        CoffeeBehaviourSetup.SetCoffee(ref coffeeCup);
                        Destroy(gameObject);
                        break;
                    case "CoffeeDrop2":
                        coffeeCup.GetComponent<CoffeeCupBehavior>().DropType = CoffeeDropType.CoffeeDrop2;
                        CoffeeBehaviourSetup.SetCoffee(ref coffeeCup);
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
        //if the player picks up the coffeebean
        if (gameObject.tag == "CoffeeBean")
        {
            //and drops it into the handgrinder
            if (col.gameObject == handGrinder && this.Grab == true)
            {
                InGrinder = true;
            }
        }

        //if the player picks up the coffeepowder
        if (gameObject.tag == "CoffeePowder")
        {
            if (col.gameObject == machineHandle && this.Grab == true)
            {
                InHandle = true;
            }
        }

        //if the player picks up the coffee machine handle
        if (gameObject.tag == "CoffeeMachineHandle")
        {
            if (col.gameObject == coffeeMachine && this.Grab == true)
            {
                InMachine = true;
            }
        }

        //if the player picks up the coffeedrop
        if (gameObject.tag == "CoffeeDrop")
        {
            if (col.gameObject == coffeeCup && this.Grab == true)
            {
                InCup = true;
            }
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (gameObject.tag == "CoffeeBean")
        {
            if (col.gameObject == handGrinder && this.Grab == true)
            {
                InGrinder = false;
            }
        }
        if (gameObject.tag == "CoffeePowder")
        {
            if (col.gameObject == machineHandle && this.Grab == true)
            {
                InHandle = false;
            }
        }

        if (gameObject.tag == "CoffeeMachineHandle")
        {
            if (col.gameObject == coffeeMachine && this.Grab == true)
            {
                InMachine = false;
            }
        }
        if (gameObject.tag == "CoffeeDrop")
        {
            if (col.gameObject == coffeeCup && this.Grab == true)
            {
                InCup = false;
            }
        }
    }
}
