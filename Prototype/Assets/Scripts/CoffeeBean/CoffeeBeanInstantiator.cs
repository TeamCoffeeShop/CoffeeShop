using UnityEngine;
//using UnityEditor; //for assetdatabase
using System.Collections;

public class CoffeeBeanInstantiator : MonoBehaviour {

    //coffeebean variables to be dragged out from the storage
    GameObject CoffeeBean1;
    GameObject CoffeeBean2;

    // Use this for initialization
    void Start () {
        CoffeeBean1 = Resources.Load<GameObject>("Prefab/CoffeeBean1");
        CoffeeBean2 = Resources.Load<GameObject>("Prefab/CoffeeBean2");
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnMouseDown()
    {
       if (gameObject.name == "CoffeeBean1Storage")
       {
           GameObject coffeeBean1 = (GameObject)Instantiate(CoffeeBean1, transform.position - new Vector3(0, 1, 0), Quaternion.identity);
           coffeeBean1.name = "CoffeeBean1";
       }

       else if (gameObject.name == "CoffeeBean2Storage")
       {
           GameObject coffeeBean2 = (GameObject)Instantiate(CoffeeBean2, transform.position - new Vector3(0, 1, 0), Quaternion.identity);
            coffeeBean2.name = "CoffeeBean2";
       }
    }
}
