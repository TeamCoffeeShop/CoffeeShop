using UnityEngine;
using System.Collections;

public class ArrowLogic : MonoBehaviour
{
    public float Speed = 50;
    public float height = 0.5f;
    Vector3 OriginalPos;
    float time = 0;

    void Start ()
    {
        OriginalPos = transform.localPosition;
    }

	void Update ()
    {
        if(InGameTime.timeScale != 0)
        {
            transform.Rotate(InGameTime.deltaTime / InGameTime.timeScale * Speed, 0, 0);
            time += InGameTime.deltaTime / InGameTime.timeScale * 3;
        }
        transform.localPosition = OriginalPos + new Vector3(0, Mathf.Sin(time) * height, 0);
	}
}
