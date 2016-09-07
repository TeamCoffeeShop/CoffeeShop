using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CustomerLogic : MonoBehaviour
{
    public GameObject OrderingBallon;
    public GameObject SpawnTimer;

    private GameObject timeofDay;

    private GameObject OB;
    private GameObject ST;

    private float timer = 0.0f;
    private float customerspawntime = 5.0f;
    //order menu instantly.
	void Start ()
    {
        timeofDay = GameObject.Find("TimeOfDay");

        //the default ballon
        if(OrderingBallon == null)
            OrderingBallon = Resources.Load<GameObject>("Prefab/OrderingBallon");

        //the default spawnTimer
        if (SpawnTimer == null)
            SpawnTimer = Resources.Load<GameObject>("Prefab/SpawnBar");

        OB = Instantiate(OrderingBallon);
        OB.transform.SetParent(GameObject.Find("UI").transform, false);
        OB.GetComponent<OrderingBallonLogic>().customer = transform;

        ST = Instantiate(SpawnTimer);
        ST.transform.SetParent(GameObject.Find("UI").transform, false);
        ST.GetComponent<CustomerSpawnTimer>().customer = transform;
        ST.GetComponent<BarScript>().MaxValue = customerspawntime;
        
        //custom cup display
        CoffeeOrderSetup.SetOrder(ref OB, GetComponent<Customer>().data.order);
    }

    void Update()
    {
        timer += (Time.deltaTime / timeofDay.GetComponent<TimeOfDay>().secondInFullDay) * 24.0f;
        ST.GetComponent<BarScript>().Value = timer;

        //Delete customer when spawn time has passed
        if (timer >= ST.GetComponent<BarScript>().MaxValue)
        {
            LeaveCoffeeShop();
        }

    }

    void LeaveCoffeeShop()
    {
        DestroyObject(ST.GetComponent<CustomerSpawnTimer>().customer.gameObject);
        DestroyObject(ST);
        DestroyObject(OB);
        DestroyObject(this.gameObject);
    }

}
