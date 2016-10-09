using UnityEngine;
using System.Collections;

public class CoffeeDrop : MonoBehaviour
{
    //time check for coffee drop
    public int dropMaxTime;
    public float rotateSpeed;
    public float rotAngle;

    //coffee drop variables
    public int PowderType = 0;
    public GameObject handle;
    public GameObject cup;
    public int CoffeeDropStep = 0;

    private float dropTime;
    private bool machineHandleCheck = false;
    private bool coffeeCupCheck = false;
    private bool highlightCoffeeCup = false;
    private int dir = 1;
    private OutlineHighlighter h2;

    public GameObject arrow;

    void Start()
    {
        h2 = transform.FindChild("Button").GetComponent<OutlineHighlighter>();
        arrow.GetComponent<Renderer>().enabled = false;
    }
    void Update()
    {
        //stick handle into position
        if (handle != null)
        {
            handle.transform.position = transform.position + new Vector3(0, 2.4f, 0);
        }

        //stick cup into position
        if (cup != null)
            cup.transform.position = transform.position + new Vector3(0, 0.7f, 0);

        switch (CoffeeDropStep)
        {
            case 0: //check if it's ready to extract coffee drop
                //highlight what's needed
                if (!highlightCoffeeCup)
                    if(machineHandleCheck && !coffeeCupCheck)
                        {
                            GameObject[] cups = GameObject.FindGameObjectsWithTag("CoffeeCup");
                            foreach (GameObject Cup in cups)
                                Cup.GetComponent<OutlineHighlighter>().highlightOn = OutlineHighlighter.HighlightOn.always;
                            highlightCoffeeCup = true;
                        arrow.GetComponent<Renderer>().enabled = true;
                    }

                //highlight button
                if (!h2.active)
                    if (machineHandleCheck && coffeeCupCheck)
                    {
                        //turn off highlight for cups
                        GameObject[] cups = GameObject.FindGameObjectsWithTag("CoffeeCup");
                        foreach (GameObject Cup in cups)
                            Cup.GetComponent<OutlineHighlighter>().highlightOn = OutlineHighlighter.HighlightOn.mouseOver;
                        h2.active = true;
                    }
                break;

            case 1: //camera rotate
                //Camera.main.GetComponent<CameraLogic>().PreviousPosition = Camera.main.GetComponent<CameraLogic>().TargetPosition;
                //Camera.main.GetComponent<CameraLogic>().TargetPosition = new Vector3(-3, 59, 1);
                //Camera.main.transform.Rotate(35, 0, 0);
                //handle.transform.Translate(0, 1.2f, 0);
                arrow.GetComponent<Renderer>().enabled = false;
                //for some reason if there's bug, return 0
                if (handle == null || cup == null)
                    CoffeeDropStep = 0;
                else
                {
                    handle.GetComponent<OutlineHighlighter>().active = false;
                    ++CoffeeDropStep;
                }
                break;

            case 2: //Check game start

                HandleMotion();
                if (handle.transform.eulerAngles.y <= 150)
                    dir = -dir;
                if (handle.transform.eulerAngles.y >= 290)
                    dir = -dir;
                handle.transform.Rotate( new Vector3(0,1,0) * Time.deltaTime * dir * 60);              
                break;

            case 3: //Drop Prepare
                //Camera.main.GetComponent<CameraLogic>().TargetPosition = Camera.main.GetComponent<CameraLogic>().PreviousPosition;
                //Camera.main.transform.Rotate(-35, 0, 0);
                //CheckGameStart = false;
                ++CoffeeDropStep;
                break;

            case 4: //Drop
                dropTime += Time.deltaTime;

                if (dropTime > dropMaxTime)
                {
                    dropTime = 0;
                    ++CoffeeDropStep;
                }
                break;

            case 5: // Exert Drop
                ExertCoffeeDrop();
                break;
        }
    }

    public void ButtonWasClicked ()
    {
        //check if button was clicked or not
        if (CoffeeDropStep == 0)
        {
            //go to next step when clicked
            if (machineHandleCheck && coffeeCupCheck)
                ++CoffeeDropStep;
        }
        else if (CoffeeDropStep == 2)
            StopHandleAndStartDrop();

    }

    public void StopHandleAndStartDrop()
    {
        if (handle.transform.eulerAngles.y < 250 && handle.transform.eulerAngles.y > 190)
        {
            h2.active = false;
            ++CoffeeDropStep;
        }
    }

    void ExertCoffeeDrop()
    {
        //reset handle, add to cup
        handle.GetComponent<CoffeeMachineHandleLogic>().DiscardPowderFromHandle();
        cup.GetComponent<CoffeeCupBehavior>().PutCoffeeDropIntoCup(PowderType);
        TakeOutCoffeeCupFromMachine();
        TakeOutCoffeeMachineHandleFromMachine();
        CoffeeDropStep = 0;
    }

    private void HandleMotion()
    {
         //Rotate +- 70 degree
        if (handle.transform.eulerAngles.y <= handle.transform.eulerAngles.y - rotAngle)
            dir = -dir;
        if (handle.transform.eulerAngles.y >= handle.transform.eulerAngles.y + rotAngle)
            dir = -dir;

        handle.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * dir * rotateSpeed);
    }

    public void PutCoffeeMachinHandleToMachine (GameObject Handle)
    {
        machineHandleCheck = true;
        handle = Handle;
        PowderType = handle.GetComponent<CoffeeMachineHandleLogic>().CoffeeBeanType;
        handle.transform.rotation = Quaternion.Euler(new Vector3(0, -137.36f, 0));
    }

    public void PutCoffeeCupToMachine (GameObject Cup)
    {
        if(!coffeeCupCheck)
        {
            coffeeCupCheck = true;
            cup = Cup;
        }
    }

    public bool TakeOutCoffeeMachineHandleFromMachine ()
    {
        if (CoffeeDropStep >= 2 && CoffeeDropStep <= 4)
            return false;

        if (handle == null)
            return false;

        PowderType = 0;
        machineHandleCheck = false;
        highlightCoffeeCup = false;
        handle.GetComponent<OutlineHighlighter>().active = true;
        handle.GetComponent<DragandDrop>().active = true;
        handle.GetComponent<Rigidbody>().isKinematic = false;
        handle = null;

        return true;
    }

    public bool TakeOutCoffeeCupFromMachine()
    {
        if (CoffeeDropStep >= 2 && CoffeeDropStep <= 4)
            return false;

        if (cup == null)
            return false;

        coffeeCupCheck = false;
        cup.GetComponent<OutlineHighlighter>().active = true;
        cup.GetComponent<DragandDrop>().active = true;
        cup = null;

        return true;
    }
}
