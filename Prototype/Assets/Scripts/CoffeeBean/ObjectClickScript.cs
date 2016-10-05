using UnityEngine;
using System.Collections;

public class ObjectClickScript : MonoBehaviour {

    public bool CheckRotation = false;

    void OnMouseDown()
    {
        CheckRotation = true;
    }

    void OnMouseUp()
    {
        CheckRotation = false;
    }

    void OnCollisionEnter()
    {
        if(gameObject.tag == "CoffeeCondition")
            CheckRotation = false;
    }
}
