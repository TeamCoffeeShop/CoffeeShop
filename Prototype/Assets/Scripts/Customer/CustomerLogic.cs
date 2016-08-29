using UnityEngine;
using System.Collections;

public class CustomerLogic : MonoBehaviour
{
    public GameObject OrderingBallon;

    //order menu instantly.
	void Start ()
    {
        //the default ballon
        if(OrderingBallon == null)
            OrderingBallon = Resources.Load<GameObject>("Prefab/OrderingBallon");

        GameObject OB = Instantiate(OrderingBallon);
        OB.transform.SetParent(GameObject.Find("UI").transform, false);
        OB.GetComponent<OrderingBallonLogic>().link = transform;
	}
}
