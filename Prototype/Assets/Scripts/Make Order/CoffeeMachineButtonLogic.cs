using UnityEngine;
using System.Collections;

public class CoffeeMachineButtonLogic : MonoBehaviour
{
    Vector3 pos;

    void Awake ()
    {
        pos = transform.position;
    }

    void OnMouseDown ()
    {
        transform.position = pos + new Vector3(0,0,0.1f);
    }

    void OnMouseUp ()
    {
        transform.position = pos;
        transform.parent.GetComponent<CoffeeDrop>().ButtonWasClicked();
    }
}
