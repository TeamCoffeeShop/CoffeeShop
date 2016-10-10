using UnityEngine;
using System.Collections;

public class CoffeeCupSelector : MonoBehaviour
{
    public GameObject CoffeeCupPrefab;
    public int active = 0;
    private float time = 0;
    private Vector3 pos;

    void Start ()
    {
        GetComponent<DragandDrop>().Target[0] = MinigameManager.Get.coffeeMachine.transform.FindChild("MachineCollider").gameObject;
        GetComponent<DragandDrop>().Highlight[0] = MinigameManager.Get.coffeeMachine.GetComponent<OutlineHighlighter>();
        GetComponent<DragandDrop>().Target[1] = MinigameManager.Get.plate.transform.FindChild("Collider").gameObject;
        GetComponent<DragandDrop>().Highlight[1] = MinigameManager.Get.plate.GetComponent<OutlineHighlighter>();
    }

    void OnMouseDown ()
    {
        //if (active == 0)
        //{
        //    time = 1;
        //    active = 1;
        //    pos = gameObject.transform.position;
        //}
    }

    void Update()
    {
        //if (active == 1)
        //{
        //    if (time < 0)
        //    {
        //        //create new cup
        //        GameObject cup = GameObject.Instantiate(CoffeeCupPrefab);
        //        cup.transform.position = pos;
        //        active = 2;
        //        time = 1000000;
        //    }
        //    else
        //        time -= InGameTime.deltaTime;
        //}
    }
}
