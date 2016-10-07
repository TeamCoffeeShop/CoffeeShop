﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic; // for list
using UnityEngine.UI;


public class HandGrinderScript : MonoBehaviour
{
    public BarScript coffeeBar;
    public Image rotationImage;
    public bool CheckGrind = false; //bool type for rotation
    public bool CheckGameStop = false; //bool type for checking coffee grinder game
    Vector3 oldEulerAngles; //bool type for rotation check
    public float totalRotation; //how much grinder's rotated
    public float stanDegree; //standard degree for coffee bean grind check
    public float stanRotation; //standard rotation for coffee powder to be made

    //coffee powder variables
    public int CoffeeType;
    public GameObject CoffeePowder1;
    public GameObject CoffeePowder2;
    public int PowderContent;

    //highlight
    OutlineHighlighter h;
    OutlineHighlighter h2;
    public GameObject CoffeeBeanOnTop;

    private bool coffeeBeanCheck = false;
    public bool IsFilled { get { return coffeeBeanCheck; } }
    private bool machineHandleCheck = false;
    public GameObject MachineHandle;
    private bool highlightMachineHandle = false;

    void Awake()
    {
        //floorMask = LayerMask.GetMask("Floor");
        //playerRigidbody = GetComponent<Rigidbody>();

        oldEulerAngles = transform.GetChild(0).rotation.eulerAngles;
        coffeeBar.Value = totalRotation;
        coffeeBar.MaxValue = stanRotation;
        coffeeBar.gameObject.SetActive(false);
        rotationImage.enabled = false;
        PowderContent = 0;
        h = GetComponent<OutlineHighlighter>();
        h2 = transform.GetChild(0).GetComponent<OutlineHighlighter>();
    }

    void Update()
    {
        //stick handle into position
        if (MachineHandle != null)
        {
            MachineHandle.transform.position = transform.position + new Vector3(0,0.4f,0);
            MachineHandle.transform.rotation = Quaternion.Euler(new Vector3(0, -137.36f, 0));
        }

        if (coffeeBeanCheck && machineHandleCheck)
        {
            h2.active = true;
            if (highlightMachineHandle)
            {
                MinigameManager.Get.coffeeMachineHandle.GetComponent<OutlineHighlighter>().highlightOn = OutlineHighlighter.HighlightOn.mouseOver;
                highlightMachineHandle = false;
            }

            if(CheckGameStop)
                //temporary grind checking
                if (totalRotation > stanRotation)
                {
                    //Camera.main.GetComponent<CameraLogic>().TargetPosition = Camera.main.GetComponent<CameraLogic>().PreviousPosition;
                    //Camera.main.transform.Rotate(-90, 0, 0);
                    PowderContent = (int)totalRotation;
                    coffeeBar.gameObject.SetActive(false);

                    totalRotation = 0;
                    ExertCoffeePowder();
                }

            //grind motion
            if (CheckGrind)
            {
                RotationCheck();

            if (!coffeeBeanCheck || (coffeeBeanCheck && machineHandleCheck))
                NewGrindMotion();
            }

        }
        else if (coffeeBeanCheck && !machineHandleCheck)
        {
            MinigameManager.Get.coffeeMachineHandle.GetComponent<OutlineHighlighter>().highlightOn = OutlineHighlighter.HighlightOn.alwaysAndOver;
            h2.active = false;
            highlightMachineHandle = true;
        }
        else
        {
            h2.active = false;
        }

        CheckGameStop = false;
    }

    void RotationCheck()
    {
        coffeeBar.gameObject.SetActive(true);

        // Save coffee content
        coffeeBar.Value = totalRotation;
        coffeeBar.MaxValue = stanRotation;
        PowderContent = (int)totalRotation;

        if (oldEulerAngles != transform.GetChild(0).rotation.eulerAngles)
        {
            //player should rotate at least certain degree to grind the coffee bean
            if (Mathf.Abs(transform.GetChild(0).rotation.eulerAngles.y - oldEulerAngles.y) >= stanDegree)
            {
                oldEulerAngles = transform.GetChild(0).rotation.eulerAngles;
                totalRotation += 1;
            }

        }
    }

    public void AddCoffeeBeanToGrinder (int type)
    {
        if (coffeeBeanCheck)
        {
            Debug.Log("There's already bean inside grinder!");
            return;
        }
        CoffeeBeanOnTop.SetActive(true);
        CoffeeType = type;
        coffeeBeanCheck = true;
    }

    public void PutCoffeeMachinHandleToGrinder (GameObject Handle)
    {
        machineHandleCheck = true;
        MachineHandle = Handle;
    }

    void ExertCoffeePowder ()
    {
        //add powder to handle
        MachineHandle.GetComponent<DragandDrop>().active = true;
        MachineHandle.GetComponent<OutlineHighlighter>().active = true;
        MachineHandle.GetComponent<CoffeeMachineHandleLogic>().CoffeeBeanType = CoffeeType;
        MachineHandle.transform.GetChild(0).gameObject.SetActive(true);

        DiscardCoffeeBean();
        TakeOutCoffeeMachineHandleFromGrinder();
    }

    public void DiscardCoffeeBean ()
    {
        //realease coffee
        CoffeeBeanOnTop.SetActive(false);
        coffeeBeanCheck = false;
        CoffeeType = 0;
    }

    public void TakeOutCoffeeMachineHandleFromGrinder ()
    {
        if (MachineHandle == null)
            return;

        machineHandleCheck = false;
        MachineHandle.GetComponent<DragandDrop>().active = true;
        MachineHandle = null;
    }

    Vector3 MousePos;
    void NewGrindMotion ()
    {
        Vector3 GrinderPos = Camera.main.WorldToScreenPoint(transform.GetChild(0).transform.position + new Vector3(0,3,0));

        float angle = Mathf.Rad2Deg * GetAngleInRadian(new Vector2(MousePos.x, MousePos.y), new Vector2(GrinderPos.x, GrinderPos.y), new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        if (angle > 0) angle = 0;

        transform.GetChild(0).Rotate(0, -angle, 0);

        MousePos = Input.mousePosition;
    }

    public static float GetAngleInRadian(Vector2 vertex1, Vector2 pivotpoint, Vector2 vertex2)
    {
        Vector2 Edge1 = (vertex1 - pivotpoint);
        Vector2 Edge2 = (vertex2 - pivotpoint);

        float angle1 = Mathf.Atan2(Edge1.y, Edge1.x);
        float angle2 = Mathf.Atan2(Edge2.y, Edge2.x);

        return angle2 - angle1;
    }

    //for object rotation following mouse
    ////////////////////////////////////
    ////////////////////////////////////
    //Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    //int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    //float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    ////////////////////////////////////
    ////////////////////////////////////


    //for coffee grinding motion
    //void GrindMotion()
    //{
    //    // Create a ray from the mouse cursor on screen in the direction of the camera.
    //    Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    // Create a RaycastHit variable to store information about what was hit by the ray.
    //    RaycastHit floorHit;

    //    // Perform the raycast and if it hits something on the floor layer...
    //    if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
    //    {
    //        // Create a vector from the player to the point on the floor the raycast from the mouse hit.
    //        Vector3 playerToMouse = floorHit.point - transform.position;

    //        // Ensure the vector is entirely along the floor plane.
    //        playerToMouse.y = 0;

    //        // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
    //        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

    //        // Set the player's rotation to this new rotation.
    //        playerRigidbody.MoveRotation(newRotation);
    //    }
    //}
}
