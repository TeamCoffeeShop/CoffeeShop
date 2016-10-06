using UnityEngine;
using System.Collections;

public class ObjectClickScript : MonoBehaviour {

    public GameObject pullHandle;
    public Transform pivotTransform;
    public bool MouseChakable = true;
    public bool CheckRotation = false;

    public Vector3 prevMousePos;
    public Vector3 curMousePos;

    public bool check = false;

    private bool buttonSelected = false;
    void OnMouseDown()
    {
        CheckRotation = true;
        prevMousePos = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        curMousePos = Input.mousePosition;
        if (MouseChakable && buttonSelected == false)
        {
            Vector3 MousePos = Input.mousePosition;
            Vector3 MousePosDelta = MousePos - prevMousePos;

            //Left
            if (Input.mousePosition.x - prevMousePos.x <150 && MousePosDelta.x > 0)
            {
                MouseChakable = false;
                pullHandle.transform.RotateAround(pivotTransform.position, new Vector3(0, 1, 0), -1* 40);
                buttonSelected = true;
            }
            //Right
            else if (Input.mousePosition.x - prevMousePos.x > -150 && MousePosDelta.x < 0)
            {
                MouseChakable = false;
                pullHandle.transform.RotateAround(pivotTransform.position, new Vector3(0, 1, 0), 1* 40);
                buttonSelected = true;
            }
        }

        if (buttonSelected)
        {

        }
    }
    void OnMouseUp()
    {
        CheckRotation = false;
        MouseChakable = true;
    }

    void OnCollisionEnter()
    {
        if(gameObject.tag == "CoffeeCondition")
            CheckRotation = false;

    }
}
