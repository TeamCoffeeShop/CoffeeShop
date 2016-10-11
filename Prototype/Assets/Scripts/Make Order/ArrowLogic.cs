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
        transform.Rotate(Time.deltaTime * Speed, 0, 0);
        transform.localPosition = OriginalPos + new Vector3(0, Mathf.Sin(time) * height, 0);
        time += Time.deltaTime * 3;
	}
}
