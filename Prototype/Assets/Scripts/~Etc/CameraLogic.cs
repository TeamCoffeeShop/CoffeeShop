using UnityEngine;
using System.Collections;

public class CameraLogic : MonoBehaviour
{
    public Vector3 OriginalPosition;
    public Vector3 TargetPosition;
    public Vector3 PreviousPosition;

    public float Speed = 1;
    public bool Stopped = false;
    bool stoptrigger = false;

	// Use this for initialization
	void Start ()
    {
        TargetPosition = OriginalPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 diff = TargetPosition - transform.position;
        if (diff.magnitude < 0.001f)
        {
            if (!stoptrigger)
            {
                Stopped = true;
                stoptrigger = true;
            }
            else
            {
                Stopped = false;
            }
        }
        else
            stoptrigger = false;

        transform.position += diff * Time.deltaTime * Speed;
	}

    //go back to OriginalPosition
    public void ResetPosition ()
    {
        TargetPosition = PreviousPosition;
    }
}
