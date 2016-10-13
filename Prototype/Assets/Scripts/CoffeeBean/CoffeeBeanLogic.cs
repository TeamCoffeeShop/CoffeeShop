using UnityEngine;
using System.Collections;

public class CoffeeBeanLogic : MonoBehaviour
{
    DragandDrop d;
    public int CoffeeBeanType;

    //GameObject arrowForGrinder;
    void Awake ()
    {
        d = GetComponent<DragandDrop>();
        /*
        arrowForGrinder = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefab/Arrow"));
        arrowForGrinder.transform.position = MinigameManager.Get.handGrinder.transform.position;
        arrowForGrinder.transform.Translate(new Vector3(0, 5, 0), Space.Self);
        */
    }

    void Update ()
    {
        //if bean is into grinder, destroy this object and turn grinder on.
        if (d.inTarget == 1)
        {
            MinigameManager.Get.handGrinder.AddCoffeeBeanToGrinder(CoffeeBeanType);
            Vector3 newPos = Camera.main.transform.position;
            newPos.x = MinigameManager.Get.handGrinder.transform.position.x;
            MinigameManager.Get.MakeOrderCamera.SetTargetLocation(newPos);
            DestroyObject(gameObject);
            //Destroy(arrowForGrinder);
        }
    }
}
