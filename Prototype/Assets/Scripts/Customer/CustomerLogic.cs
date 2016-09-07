using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CustomerLogic : MonoBehaviour
{
    public GameObject OrderingBallon;
    public GameObject SpawnTimer;

    private GameObject ST;

    float time = 0.0f;
    //order menu instantly.
	void Start ()
    {
        //the default ballon
        if(OrderingBallon == null)
            OrderingBallon = Resources.Load<GameObject>("Prefab/OrderingBallon");

        //the default spawnTimer
        if (SpawnTimer == null)
            SpawnTimer = Resources.Load<GameObject>("Prefab/SpawnBar");

        GameObject OB = Instantiate(OrderingBallon);
        OB.transform.SetParent(GameObject.Find("UI").transform, false);
        OB.GetComponent<OrderingBallonLogic>().customer = transform;

        ST = Instantiate(SpawnTimer);
        ST.transform.SetParent(GameObject.Find("UI").transform, false);
        ST.GetComponent<CustomerSpawnTimer>().customer = transform;
        
        //custom cup display
        CoffeeOrderSetup.SetOrder(ref OB, GetComponent<Customer>().data.order);
    }

    void Update()
    {
        time += 0.005f;
        ST.GetComponent<BarScript>().Value = time;
        ST.GetComponent<BarScript>().MaxValue = 1.0f;
    }
}
