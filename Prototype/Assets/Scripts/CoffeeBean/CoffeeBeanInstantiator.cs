using UnityEngine;
//using UnityEditor; //for assetdatabase
using System.Collections;

public class CoffeeBeanInstantiator : MonoBehaviour {

    //coffeebean variables to be dragged out from the storage
    public GameObject CoffeeBean;

    void OnMouseDown()
    {
           GameObject coffeeBean = (GameObject)Instantiate(CoffeeBean, transform.position - new Vector3(0, 1, 0), Quaternion.identity);
    }
}
