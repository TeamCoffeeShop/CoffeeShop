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

    private int dropTime;
    private bool machineHandleCheck = false;
    private bool coffeeCupCheck = false;
    private int dir = 1;

    void Update()
    {
        //stick handle into position
        if (handle != null)
            handle.transform.position = transform.position + new Vector3(0, 1.9f, 0);

        //stick cup into position
        if (cup != null)
            cup.transform.position = transform.position + new Vector3(0, 0.7f, 0);

        switch (CoffeeDropStep)
        {
            case 0: //check if it's ready to extract coffee drop
                break;

            case 1: //camera rotate
                //Camera.main.GetComponent<CameraLogic>().PreviousPosition = Camera.main.GetComponent<CameraLogic>().TargetPosition;
                //Camera.main.GetComponent<CameraLogic>().TargetPosition = new Vector3(-3, 59, 1);
                //Camera.main.transform.Rotate(35, 0, 0);
                //handle.transform.Translate(0, 1.2f, 0);

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
                ExertCoffeeDrop();
                break;
        }
    }

    void ExertCoffeeDrop()
    {
        dropTime += 1;

        if (dropTime > dropMaxTime)
        {
            dropTime = 0;

            //reset handle
            handle.GetComponent<CoffeeMachineHandleLogic>().CoffeeBeanType = 0;
            handle.transform.GetChild(0).gameObject.SetActive(false);

            TakeOutCoffeeMachineHandleFromMachine();

            //add to cup
            switch(PowderType)
            {
                case 1:
                    cup.GetComponent<CoffeeCupBehavior>().DropType = CoffeeDropType.CoffeeDrop1;
                    if (cup.transform.childCount != 0)
                        cup.transform.GetChild(0).gameObject.SetActive(true);
                    break;
                case 2:
                    cup.GetComponent<CoffeeCupBehavior>().DropType = CoffeeDropType.CoffeeDrop2;
                    if (cup.transform.childCount != 0)
                        cup.transform.GetChild(0).gameObject.SetActive(true);
                    break;
                default:
                    break;

            }
            TakeOutCoffeeCupFromMachine();
            CoffeeDropStep = 0;
        }
    }

    void OnMouseDown ()
    {
        if (CoffeeDropStep == 0)
        {
            //highlight what's required
            if (machineHandleCheck && !coffeeCupCheck)
            {
                GameObject[] cups = GameObject.FindGameObjectsWithTag("CoffeeCup");
                foreach (GameObject cup in cups)
                    cup.GetComponent<OutlineHighlighter>().highlightOn = OutlineHighlighter.HighlightOn.always;
            }

        }
        else if (CoffeeDropStep == 2)
        {
            if (handle.transform.eulerAngles.y < 250 && handle.transform.eulerAngles.y > 190)
                ++CoffeeDropStep;
        }
    }

    void OnMouseUp ()
    {
        if (CoffeeDropStep == 0)
        {
            //go to next step when clicked
            if (machineHandleCheck && coffeeCupCheck)
                ++CoffeeDropStep;
            //highlight what's required
            else if (machineHandleCheck)
            {
                GameObject[] cups = GameObject.FindGameObjectsWithTag("CoffeeCup");
                foreach (GameObject cup in cups)
                    cup.GetComponent<OutlineHighlighter>().highlightOn = OutlineHighlighter.HighlightOn.mouseOver;
            }
        }
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
        if (CoffeeDropStep >= 2)
            return false;

        PowderType = 0;
        machineHandleCheck = false;
        handle.GetComponent<OutlineHighlighter>().active = true;
        handle = null;

        return true;
    }

    public bool TakeOutCoffeeCupFromMachine()
    {
        if (CoffeeDropStep >= 2)
            return false;

        coffeeCupCheck = false;
        cup.GetComponent<OutlineHighlighter>().active = true;
        cup = null;

        return true;
    }
}
