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

    bool ready;

    //time check for coffee drop
    int dropTime;
    public int dropMaxTime;

    //coffee drop variables
    public GameObject CoffeeDrop1;
    public GameObject CoffeeDrop2;

    // Use this for initialization
    void Start()
    {
        ready = false;
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
}
