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

    //coffee machine handle
    private GameObject handle;
    private bool CameraRotate;
    private bool MinigamePrepare;
    public float angle = 0;
    bool increaseAngle = true;

    //coffee drop variables
    public GameObject CoffeeDrop1;
    public GameObject CoffeeDrop2;

    // Use this for initialization
    void Start()
    {
        ready = false;
        handle = GameObject.Find("EspressoMachineHandle");
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
                Camera.main.GetComponent<CameraLogic>().TargetPosition = new Vector3(-3, 9, 1);
                Camera.main.transform.Rotate(40, 0, 0);
                handle.transform.Translate(0, 1, 0);
                CameraRotate = false;
                MinigamePrepare = true;
            }
            if (MinigamePrepare)
            {
                HandleMotion();
            }
            //DropCoffee();
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
                GameObject coffeedrop1 = (GameObject)Instantiate(CoffeeDrop1, transform.position + new Vector3(0, 5, 0), Quaternion.identity);
                coffeedrop1.name = "CoffeeDrop1";
            }

            if (CoffeePowders[0].CPowder == 2)
            {
                GameObject coffeedrop2 = (GameObject)Instantiate(CoffeeDrop2, transform.position + new Vector3(0, 5, 0), Quaternion.identity);
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
            if (gameObject.name == "CoffeeMachine")
                CameraRotate = true;
        }
    }

    void HandleMotion()
    {

    }
}
