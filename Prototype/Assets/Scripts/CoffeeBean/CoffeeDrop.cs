using UnityEngine;
using System.Collections;
using System.Collections.Generic; // for list

public class CoffeePowder
{

    public bool Check;
    public int CPowder;

    public CoffeePowder(bool check, int coffeePowder)
    {
        Check = check;
        CPowder = coffeePowder;
    }
}

public class CoffeeDrop : MonoBehaviour
{
    public List<CoffeePowder> CoffeePowders = new List<CoffeePowder>();

    public bool ready;

    //time check for coffee drop
    int dropTime;
    public int dropMaxTime;
    public float angle;

    private GameObject handleObj;
    //coffee machine handle
    private GameObject handle;
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
    public GameObject CoffeeDrop1;
    public GameObject CoffeeDrop2;

    // Use this for initialization
    void Start()
    {
        ready = false;
        handle = GameObject.Find("EspressoMachineHandle");
        handleObj = GameObject.Find("CoffeeMachineHandle");
        handleObj.SetActive(true);
        handle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //when the coffee machine is not empty
        if (CoffeePowders.Count != 0)
        {
            ready = true;
        }

        if (ready == true)
        {
            if (CameraRotate)
            {
                Camera.main.GetComponent<CameraLogic>().PreviousPosition = Camera.main.GetComponent<CameraLogic>().TargetPosition;
                Camera.main.GetComponent<CameraLogic>().TargetPosition = new Vector3(-3, 59, 1);
                Camera.main.transform.Rotate(35, 0, 0);
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
                Camera.main.GetComponent<CameraLogic>().TargetPosition = Camera.main.GetComponent<CameraLogic>().PreviousPosition;
                Camera.main.transform.Rotate(-35, 0, 0);
                CheckGameStart = false;
                DropPrepare = false;
                ready = false;
                Drop = true;
            }
            if (Drop)
                DropCoffee();
        }
    }

    void DropCoffee()
    {
        dropTime += 1;

        if (dropTime > dropMaxTime)
        {
            dropTime = 0;
            if (CoffeePowders[0].CPowder == 1)
            {
                GameObject coffeedrop1 = (GameObject)Instantiate(CoffeeDrop1, transform.position + new Vector3(0, 20, 0), Quaternion.identity);
                coffeedrop1.name = "CoffeeDrop1";
            }

            if (CoffeePowders[0].CPowder == 2)
            {
                GameObject coffeedrop2 = (GameObject)Instantiate(CoffeeDrop2, transform.position + new Vector3(0, 20, 0), Quaternion.identity);
                coffeedrop2.name = "CoffeeDrop2";
            }

            CoffeePowders.Clear();
            ready = false;
        }
    }

    void OnMouseDown()
    {
        if (ready)
        {
            //if (MinigamePrepare == false && CheckGameStart == false)
            //{
            //    if (gameObject.name == "CoffeeMachine")
            //        CameraRotate = true;
            //}
        }

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
}
