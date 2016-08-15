using UnityEngine;
using UnityEditor;
using System.Collections;

public class CustomerLogic : MonoBehaviour
{
    public GameObject OrderingBallon;

    //order menu instantly.
	void Start ()
    {
        //the default ballon
        if(OrderingBallon == null)
            OrderingBallon = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Prefab/OrderingBallon.prefab");

        Instantiate(OrderingBallon, transform.position + new Vector3(0,transform.localScale.y * 2,0), Quaternion.identity);
	}
}
