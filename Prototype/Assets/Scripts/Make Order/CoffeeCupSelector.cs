using UnityEngine;
using System.Collections;

public class CoffeeCupSelector : MonoBehaviour
{
    public GameObject CoffeeCupPrefab;
    public bool active;
    private float time = 0;
    private Vector3 pos;

    void Start ()
    {
        active = true;
        GetComponent<DragandDrop>().Target[0] = MinigameManager.Get.coffeeMachine.gameObject;
    }

    void OnMouseDown ()
    {
        if (active)
        {
            time = 1;
            active = false;
            pos = gameObject.transform.position;
        }
    }

    void Update()
    {
        if (!active)
        {
            if (time < 0)
            {
                //create new cup
                GetComponent<Collider>().isTrigger = false;
                GetComponent<Rigidbody>().isKinematic = false;
                GameObject cup = GameObject.Instantiate(CoffeeCupPrefab);
                cup.transform.position = pos;
                time = 100000;
            }
            else
            {
                time -= InGameTime.deltaTime;
            }
        }
    }
}
