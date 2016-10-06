using UnityEngine;
using System.Collections;

public class ObjectClickScript : MonoBehaviour {

    public bool MouseChakable = true;
    public bool CheckRotation = false;

    public Vector3 prevMousePos;
    void OnMouseDown()
    {
        CheckRotation = true;
        prevMousePos = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        if (MouseChakable)
        {
            if (Input.mousePosition.x - prevMousePos.x <2)
            {
                MouseChakable = false;
            }

            else if (Input.mousePosition.x - prevMousePos.x < -2)
            {
                MouseChakable = false;
            }
        }
    }
    void OnMouseUp()
    {
        CheckRotation = false;
        //MouseChakable = true;
    }

    void OnCollisionEnter()
    {
        if(gameObject.tag == "CoffeeCondition")
            CheckRotation = false;
    }
}
