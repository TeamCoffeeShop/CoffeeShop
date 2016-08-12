using UnityEngine;
using UnityEditor;
using System.Collections;

public class CustomerLogic : MonoBehaviour
{
    public GameObject OrderingBallon = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Prefab/OrderingBallon.prefab");

    //order menu instantly.
	void Start ()
    {
        Instantiate(OrderingBallon, transform.position + new Vector3(0,transform.localScale.y * 2,0), Quaternion.identity);
	}
}
