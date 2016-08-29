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

        Instantiate(OrderingBallon, transform.position + new Vector3(0,transform.localScale.y * 2,0), Quaternion.identity);
	}
}
