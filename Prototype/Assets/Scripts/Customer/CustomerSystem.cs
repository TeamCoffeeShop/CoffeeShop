using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomerSystem : MonoBehaviour {

    //customer List
    public GameObject CustomerListObj;

    public Button MiniGameButton;

    //For Random Time
    public float maxTime;
    public float minTime;
    //The time to spawn the customer
    public float spawnTime;
    //current time
    public float time;
    
    //Seats list
    public GameObject CustomerSeats;

    //Coffee Shop Floor
    public GameObject Floor;

    //Customer Prefab
    public const string customerPath = "Prefab/Customer2";
    //Customer List Prefab
    public GameObject CustomerListPrefab; 

    //UI
    private string menuText;

    void Awake()
    {
        CustomerListObj = GameObject.Find("CustomerList(Clone)");
        if (CustomerListObj == null)
        {
            CustomerListObj = Instantiate<GameObject>(CustomerListPrefab);
        }
        else
        {
            foreach (CustomerData data in CustomerListObj.GetComponent<CustomerContainer>().customers)
            {
                CreateCustomer(data, CustomerSystem.customerPath,
                    new Vector3(data.posx, data.posy, data.posz), Quaternion.identity);
            }
        }

    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(CustomerListObj);
        SelectMenu();
        SetRandomTime();
        time = minTime;

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

    void SelectMenu()
    {
        int randomNum = Random.Range(0, 2);
        // Americano
        if (randomNum == 0)
        {
            menuText = "Americano";
        }
        else if (randomNum == 1)
        {
            menuText = "Caffe latte";
        }
        else if (randomNum == 2)
        {
            menuText = "Caffe Mocha";
        }
    }

    Customer CreateCustomer(string path, Vector3 position, Quaternion rotation)
    {
        GameObject prefab = Resources.Load<GameObject>(path);

        GameObject go = GameObject.Instantiate(prefab, position, rotation) as GameObject;

        Customer customer = go.GetComponent<Customer>() ?? go.AddComponent<Customer>();

        customer.StoreData();

        CustomerListObj.GetComponent<CustomerContainer>().customers.Add(customer.data);

        return customer;

    }

    public static void CreateCustomer(CustomerData data, string path, Vector3 position, Quaternion rotation)
    {
        GameObject prefab = Resources.Load<GameObject>(path);

        GameObject go = GameObject.Instantiate(prefab, position, rotation) as GameObject;

        Customer customer = go.GetComponent<Customer>() ?? go.AddComponent<Customer>();
        customer.StoreData();
        
        customer.data = data;
    }

    void SpawnCustomer()
    {
        //counts up
        time += Time.deltaTime;

        // Check whether it's time to spawn the customer
        if (time >= spawnTime)
        {
            

            //setting customer's position. change this after getting real model.
            Vector3 new_customer_pos = new Vector3(Floor.transform.position.x, Floor.transform.position.y + Floor.transform.localScale.y * 0.5f + 6.0f, Floor.transform.position.z);
            //SpawnInRandomPos(ref new_customer_pos);
            SpawnInRandomDefinedPos(ref new_customer_pos);
            Customer customer = CreateCustomer(customerPath, new_customer_pos, Quaternion.identity);

            //set customer's order.
            SetRandomOrder(ref customer);
            SetRandomTime();
            time = 0;
        }
    }

    void SpawnInRandomPos(ref Vector3 pos)
    {
        Vector2 new_customer_pos_random_range = new Vector2(Floor.transform.localScale.x * 0.5f, Floor.transform.localScale.z * 0.5f);
        const float new_customer_pos_range_offset = 1;

        new_customer_pos_random_range.x -= new_customer_pos_range_offset;
        new_customer_pos_random_range.y -= new_customer_pos_range_offset;

        //set random position based on floor's scale and offset
        pos.x += Random.Range(-new_customer_pos_random_range.x, new_customer_pos_random_range.x);
        pos.z += Random.Range(-new_customer_pos_random_range.y, new_customer_pos_random_range.y);
    }

    void SpawnInRandomDefinedPos(ref Vector3 pos)
    {
        int size = CustomerSeats.transform.childCount;

        //if there's no seat, return
        if (size == 0)
            return;

        int spawnseat = Random.Range(0, size);

        //set position to the seat
        Vector3 newpos = CustomerSeats.transform.GetChild(spawnseat).transform.position;
        pos.x = newpos.x;
        pos.z = newpos.z;
    }

    void SetRandomOrder(ref Customer customer)
    {
        //for right now, we'll only distinguish droptype.
        switch(Random.Range(0, 2))
        {
            case 0:
                customer.order.DropType = CoffeeDropType.CoffeeDrop1;
                break;
            case 1:
                customer.order.DropType = CoffeeDropType.CoffeeDrop2;
                break;
            default:
                break;
        }
    }
}
