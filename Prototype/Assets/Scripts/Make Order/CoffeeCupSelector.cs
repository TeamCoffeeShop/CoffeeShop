using UnityEngine;
using System.Collections;

public class CoffeeCupSelector : MonoBehaviour
{
    public GameObject CoffeeCupPrefab;

    void Start ()
    {
        GetComponent<DragandDrop>().Target[0] = MinigameManager.Get.coffeeMachine.gameObject;
    }

    void OnMouseDown ()
    {
        //create new cup
        GetComponent<Collider>().isTrigger = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GameObject cup = GameObject.Instantiate(CoffeeCupPrefab);
        cup.transform.position = gameObject.transform.position;
        Destroy(this);
    }
}
