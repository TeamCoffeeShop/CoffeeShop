using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CustomerLogic : MonoBehaviour
{
    public GameObject OrderingBallon;
    public GameObject SpawnTimer;
    public Vector3 TargetSeat;

    bool arrived = false;
    float walkSpeed = 20;
    private GameObject timeofDay;

    private GameObject OB;
    private GameObject ST;

    private float timer = 0.0f;
    private float customerspawntime = 5.0f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnLevelWasLoaded(int level)
    {
        //if not main level, deactivate customers
        if (level != Scenes.MainLevel)
            GetComponent<Renderer>().enabled = false;
        else
            GetComponent<Renderer>().enabled = true;
    }

    //order menu instantly.
    void Start ()
    {
        timeofDay = GameObject.Find("TimeOfDay");

        
        //set Y in first place
        transform.Translate(0, TargetSeat.y - transform.position.y, 0);

        //the default ballon
        if(OrderingBallon == null)
            OrderingBallon = Resources.Load<GameObject>("Prefab/OrderingBallon");

        //the default spawnTimer
        if (SpawnTimer == null)
            SpawnTimer = Resources.Load<GameObject>("Prefab/SpawnBar");
    }

    void Update()
    {
        //if not arrived, walk
        if (!arrived)
        {
            if (WalkTowardsSeat())
            {
                //after arriving, make order
                OrderStart();
                arrived = true;
            }
        }
        else
        {
            if (GetComponent<Renderer>().enabled == true)
            {
                timer += (Time.deltaTime / timeofDay.GetComponent<TimeOfDay>().secondInFullDay) * 24.0f;
                
                if(ST)
                {
                    ST.GetComponent<BarScript>().Value = timer;

                    ////Delete customer when spawn time has passed
                    if (timer >= ST.GetComponent<BarScript>().MaxValue)
                    {
                        LeaveCoffeeShop();
                    }
                }
            }
        }
    }

    void OrderStart ()
    {
        OB = Instantiate(OrderingBallon);
        OB.transform.SetParent(GameObject.Find("[OrderHUD]").transform, false);
        OB.GetComponent<OrderingBallonLogic>().customer = transform;

        ST = Instantiate(SpawnTimer);
        ST.transform.SetParent(GameObject.Find("[OrderHUD]").transform, false);
        ST.GetComponent<CustomerSpawnTimer>().customer = transform;
        ST.GetComponent<BarScript>().MaxValue = customerspawntime;
        
        //custom cup display
        CoffeeOrderSetup.SetOrder(ref OB, GetComponent<Customer>().data.order);
    }

    void LeaveCoffeeShop ()
    {
        DestroyObject(ST.GetComponent<CustomerSpawnTimer>().customer.gameObject);
        DestroyObject(ST);
        DestroyObject(OB);
        DestroyObject(this.gameObject);
    }

    //walk to the seat
    bool WalkTowardsSeat ()
    {
        //walk x coord first
        if (transform.position.x < TargetSeat.x)
        {
            transform.Translate(walkSpeed * Time.deltaTime, 0, 0);

            //if over, stop
            if (transform.position.x > TargetSeat.x)
                transform.position = new Vector3(TargetSeat.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > TargetSeat.x)
        {
            transform.Translate(-walkSpeed * Time.deltaTime, 0, 0);

            //if over, stop
            if (transform.position.x < TargetSeat.x)
                transform.position = new Vector3(TargetSeat.x, transform.position.y, transform.position.z);
        }
        //if x finished, walk y
        else if (transform.position.z < TargetSeat.z)
        {
            transform.Translate(0, 0, walkSpeed * Time.deltaTime);

            //if over, stop
            if (transform.position.z > TargetSeat.z)
                transform.position = new Vector3(transform.position.x, transform.position.y, TargetSeat.z);
        }
        else if (transform.position.z > TargetSeat.z)
        {
            transform.Translate(0, 0, -walkSpeed * Time.deltaTime);

            //if over, stop
            if (transform.position.z < TargetSeat.z)
                transform.position = new Vector3(transform.position.x, transform.position.y, TargetSeat.z);
        }
        else
            return true;
        return false;
    }
}
