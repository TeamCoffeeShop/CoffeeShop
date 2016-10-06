using UnityEngine;
using System.Collections;

public class CoffeeBeanLogic : MonoBehaviour
{
    DragandDrop d;
    public int CoffeeBeanType;
    
    void Awake ()
    {
        d = GetComponent<DragandDrop>();
    }

    void Update ()
    {
        //if bean is into grinder, destroy this object and turn grinder on.
        if(d.inTarget == 1)
        {
            MinigameManager.Get.handGrinder.AddCoffeeBeanToGrinder(CoffeeBeanType);
            DestroyObject(gameObject);
        }
    }
}
