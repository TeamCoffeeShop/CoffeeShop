using UnityEngine;
using System.Collections;

public class CameraLogic : MonoBehaviour
{
    public Vector3 OriginalPosition;
    public Vector3 TargetPosition;
    public Vector3 PreviousPosition;

    public float Speed = 1;

	// Use this for initialization
	void Start ()
    {
        TargetPosition = OriginalPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += (TargetPosition - transform.position) * Time.deltaTime * Speed;
	}

    //go back to OriginalPosition
    public void ResetPosition ()
    {
        TargetPosition = PreviousPosition;
    }
}
