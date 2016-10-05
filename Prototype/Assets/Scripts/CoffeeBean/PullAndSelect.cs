using UnityEngine;
using System.Collections;

public class PullAndSelect : MonoBehaviour {
    public GameObject sphere; 
    public Transform pivotTransform;
    //bool type for rotation check
    bool CheckRotation;

    // Use this for initialization
    void Start () {

        CheckRotation = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (sphere.GetComponent<ObjectClickScript>().CheckRotation)
            PullHandleMotion();
    }

    Vector3 PreviousMousePos;

    void PullHandleMotion()
    {
        if (Object.Equals(PreviousMousePos, default(Vector3)))
        {
            PreviousMousePos = Input.mousePosition;
        }
        Vector3 MousePos = Input.mousePosition;
        Vector3 MousePosDelta = MousePos - PreviousMousePos;
        Vector3 CylinderPos = Camera.main.WorldToScreenPoint(transform.GetChild(0).transform.position + new Vector3(0, 3, 0));

        float angle = Mathf.Rad2Deg * GetAngleInRadian(new Vector2(MousePos.x, MousePos.y), new Vector2(pivotTransform.position.x, pivotTransform.position.y), new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        //if (angle > -180 && angle < 180)
        
            transform.RotateAround(pivotTransform.position, new Vector3(0, 1, 0), -MousePosDelta.x);

        PreviousMousePos = MousePos;
    }

    public static float GetAngleInRadian(Vector2 vertex1, Vector2 pivotpoint, Vector2 vertex2)
    {
        Vector2 Edge1 = (vertex1 - pivotpoint);
        Vector2 Edge2 = (vertex2 - pivotpoint);

        float angle1 = Mathf.Atan2(Edge1.y, Edge1.x);
        float angle2 = Mathf.Atan2(Edge2.y, Edge2.x);

        return angle2 - angle1;
    }
}
