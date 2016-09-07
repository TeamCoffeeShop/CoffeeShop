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

    private GameObject ST;

    float time = 0.0f;
    //order menu instantly.
	void Start ()
    {
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
            {
                //after arriving, make order
                OrderStart();
                arrived = true;
            }
        }
        else
        {
            time += 0.005f;
            ST.GetComponent<BarScript>().Value = time;
            ST.GetComponent<BarScript>().MaxValue = 1.0f;
        }
    }

    void OrderStart ()
    {
        GameObject OB = Instantiate(OrderingBallon);
        OB.transform.SetParent(GameObject.Find("UI").transform, false);
        OB.GetComponent<OrderingBallonLogic>().customer = transform;

        ST = Instantiate(SpawnTimer);
        ST.transform.SetParent(GameObject.Find("UI").transform, false);
        ST.GetComponent<CustomerSpawnTimer>().customer = transform;

        //custom cup display
        CoffeeOrderSetup.SetOrder(ref OB, GetComponent<Customer>().data.order);
    }
}
