using UnityEngine;
using System.Collections;

public class ObjectClickScript : MonoBehaviour {

    //Water & Milk Instantiator
    private WaterMilkInstantiator Instantiator;

    public GameObject pullHandle;
    public Transform pivotTransform;
    public bool MouseChakable = true;
    public bool CheckRotation = false;

    public Vector3 prevMousePos;
    public Vector3 curMousePos;

    public bool check = false;

    private bool buttonLeftSelected = false;
    private bool buttonRightSelected = false;
    void Start()
    {
        Instantiator = GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>();
    }

    void OnMouseDown()
    {
        CheckRotation = true;
        prevMousePos = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        curMousePos = Input.mousePosition;

        if (MouseChakable)
        {
            Vector3 MousePos = Input.mousePosition;
            Vector3 MousePosDelta = MousePos - prevMousePos;

            //Select Right
            if (Input.mousePosition.x - prevMousePos.x <150 && MousePosDelta.x > 0)
            {
                MouseChakable = false;
                if (buttonLeftSelected)
                {
                    pullHandle.transform.RotateAround(pivotTransform.position, new Vector3(0, 1, 0), -1 * 40);
                    buttonLeftSelected = false;
                }
                if(buttonLeftSelected == false && buttonRightSelected == false)
                    pullHandle.transform.RotateAround(pivotTransform.position, new Vector3(0, 1, 0), -1* 40);
                buttonRightSelected = true;
                if (gameObject.name == "HotIceButton")
                {
                    Instantiator.HotIceType = HotIceType.Ice;
                    Instantiator.Ready = true;
                    Debug.Log("Ice Selected");
                }
                if (gameObject.name == "WaterMilkButton")
                {
                    Instantiator.WaterMilkType = WaterMilkType.Milk;
                    Instantiator.Ready = true;
                    Debug.Log("Milk Selected");
                }
            }
            //Left
            else if (Input.mousePosition.x - prevMousePos.x > -150 && MousePosDelta.x < 0)
            {
                MouseChakable = false;
                if (buttonRightSelected)
                {
                    pullHandle.transform.RotateAround(pivotTransform.position, new Vector3(0, 1, 0), 1 * 40);
                    buttonRightSelected = false;
                }
                if (buttonLeftSelected == false && buttonRightSelected == false)
                    pullHandle.transform.RotateAround(pivotTransform.position, new Vector3(0, 1, 0), 1* 40);
                buttonLeftSelected = true;
                if (gameObject.name == "HotIceButton")
                {
                    Instantiator.HotIceType = HotIceType.Hot;
                    Instantiator.Ready = true;
                    Debug.Log("Hot Selected");
                }
                if (gameObject.name == "WaterMilkButton")
                {
                    Instantiator.WaterMilkType = WaterMilkType.Water;
                    Instantiator.Ready = true;
                    Debug.Log("Water Selected");
                }
            }
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
