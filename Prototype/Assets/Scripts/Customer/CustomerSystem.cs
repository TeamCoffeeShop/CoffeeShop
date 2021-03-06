﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomerSystem : MonoBehaviour
{
    //door that customer enters
    public GameObject Enterance;

    //For Random Time
    public float maxTime;
    public float minTime;
    //The time to spawn the customer
    public float spawnTime;
    //current time
    public float time;
    
    //Seats list
    public GameObject CustomerSeats;

    //Customer Prefab
    public const string customerPath = "Prefab/Customer/Customer";
    //Customer List Prefab
    public GameObject CustomerListPrefab; 

	void Start ()
    {
        SetRandomTime();
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnCustomer();
    }

    void SetRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

    Customer CreateCustomer(string path, Vector3 position, OrderType ordertype, Quaternion rotation)
    {
        GameObject prefab = Resources.Load<GameObject>(path);

        GameObject go = GameObject.Instantiate(prefab, position, rotation) as GameObject;

        Customer customer = go.GetComponent<Customer>() ?? go.AddComponent<Customer>();
        customer.order = ordertype;
        customer.StoreData();

        return customer;

    }

    public static void CreateCustomer(CustomerData data, string path, Vector3 position, OrderType ordertype, Quaternion rotation)
    {
        GameObject prefab = Resources.Load<GameObject>(path);

        GameObject go = GameObject.Instantiate(prefab, position, rotation) as GameObject;

        Customer customer = go.GetComponent<Customer>() ?? go.AddComponent<Customer>();
        customer.order = ordertype;
        customer.StoreData();
        
        customer.data = data;
    }

    void SpawnCustomer()
    {
        //counts up
        if(!MainGameManager.Get.OnDialogue)
            time += InGameTime.deltaTime;

        // Check whether it's time to spawn the customer
        if (time >= spawnTime)
        {            
            Grid enterGrid = Grid.FindClosestGrid(Enterance.transform.position);

            Grid DefinedSeat = SetRandomSeat();
            //if no seat, return
            
            if (DefinedSeat == null)
            {
                SetRandomTime();
                time = 0;
                return;
            }

            //Set customer's order
            OrderType order = SetRandomOrder();
            // Create customer and add customer to customer list
            Customer customer = CreateCustomer(customerPath, enterGrid.transform.position, order, Quaternion.identity);
            customer.order = order;
            customer.GetComponent<CustomerLogic>().SeatX = DefinedSeat.X;
            customer.GetComponent<CustomerLogic>().SeatZ = DefinedSeat.Z;
            customer.GetComponent<CustomerLogic>().Begin = enterGrid;
            customer.GetComponent<CustomerLogic>().End = DefinedSeat;
            customer.transform.Rotate(0, -90, 0,Space.World);
            SetRandomTime();
            time = 0;
        }
    }

    Grid SetRandomSeat ()
    {
        int size = MainGameManager.Get.Floor.EmptySeats.Count;

        //if there's no seat, return
        if (size == 0)
            return null;

        int spawnseat = Random.Range(0, size);

        Grid seat = MainGameManager.Get.Floor.EmptySeats[spawnseat].GetComponentInParent<Grid>();
        seat.transform.GetChild(0).GetComponent<CafeDeco>().Filled = true;

        //set position to the seat
        return seat;

    }

    OrderType SetRandomOrder()
    {
        OrderType ordertype = OrderType.None;

        //for right now, we'll only distinguish droptype.
        switch(Random.Range(0, 10))
        {
            case 0:
            case 2:
            case 4:
            case 6:
            case 8:
                ordertype = OrderType.HotAmericano;
                break;
            case 1:
            case 3:
            case 5:
            case 7:
            case 9:
                ordertype = OrderType.IceAmericano;
                break;
            default:
                break;
        }

        return ordertype;

    }
}
