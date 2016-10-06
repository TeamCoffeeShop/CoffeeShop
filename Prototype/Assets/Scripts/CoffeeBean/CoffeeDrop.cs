using UnityEngine;
using System.Collections;
using System.Collections.Generic; // for list

public class CoffeePowder
{

    public bool Check;
    public int CPowder;
    public int coffeeContent;

    public CoffeePowder(bool check, int coffeePowder, int content)
    {
        Check = check;
        CPowder = coffeePowder;
        coffeeContent = content;
    }
}

public class CoffeeDrop : MonoBehaviour
{
    //time check for coffee drop
    int dropTime;
    public int dropMaxTime;
    public float angle;

    //coffee machine handle
    public bool CameraRotate;
    private bool MinigamePrepare;
    private bool CheckGameStart = false;
    private bool DropPrepare = false;
    private bool Drop = false;
    public float eulerangle = 0;

    public float rotateSpeed;
    public float rotAngle;
    private int dir = 1;

    //coffee drop variables
    public int PowderType = 0;
    private bool machineHandleCheck = false;
    private GameObject handle;

    void Update()
    {
        //stick handle into position
        if (handle != null)
            handle.transform.position = transform.position + new Vector3(0, 1.9f, 0);

        if (CameraRotate)
        {
            //Camera.main.GetComponent<CameraLogic>().PreviousPosition = Camera.main.GetComponent<CameraLogic>().TargetPosition;
            //Camera.main.GetComponent<CameraLogic>().TargetPosition = new Vector3(-3, 59, 1);
            //Camera.main.transform.Rotate(35, 0, 0);
            handle.transform.Translate(0, 1.2f, 0);
            CameraRotate = false;
            MinigamePrepare = true;
        }
        if (MinigamePrepare)
        {
            handle.SetActive(true);
            HandleMotion();
            MinigamePrepare = false;
            CheckGameStart = true;
        }
        if (CheckGameStart)
        {
            HandleMotion();
            if (handle.transform.eulerAngles.y <= 150)
                dir = -dir;
            if (handle.transform.eulerAngles.y >= 290)
                dir = -dir;

            eulerangle = handle.transform.eulerAngles.y;
            handle.transform.Rotate( new Vector3(0,1,0) * Time.deltaTime * dir * 60);              
        }
        if (DropPrepare)
        {
            //Camera.main.GetComponent<CameraLogic>().TargetPosition = Camera.main.GetComponent<CameraLogic>().PreviousPosition;
            //Camera.main.transform.Rotate(-35, 0, 0);
            CheckGameStart = false;
            DropPrepare = false;
            Drop = true;
        }
        if (Drop)
            DropCoffee();
    }

    void DropCoffee()
    {
        dropTime += 1;

        if (dropTime > dropMaxTime)
        {
            dropTime = 0;
            //if (CoffeePowders[0].CPowder == 1)
            //{
            //    GameObject coffeedrop1 = (GameObject)Instantiate(CoffeeDrop1, transform.position + new Vector3(0, 20, 0), Quaternion.identity);
            //    coffeedrop1.name = "CoffeeDrop1";
            //}

            //if (CoffeePowders[0].CPowder == 2)
            //{
            //    GameObject coffeedrop2 = (GameObject)Instantiate(CoffeeDrop2, transform.position + new Vector3(0, 20, 0), Quaternion.identity);
            //    coffeedrop2.name = "CoffeeDrop2";
            //}
        }
    }

    void OnMouseDown()
    {
        //if (MinigamePrepare == false && CheckGameStart == false)
        //{
        //    if (gameObject.name == "CoffeeMachine")
        //        CameraRotate = true;
        //}

        if (CheckGameStart)
        {
            if (handle.transform.eulerAngles.y < 250 && handle.transform.eulerAngles.y > 190)
                DropPrepare = true;
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
}
