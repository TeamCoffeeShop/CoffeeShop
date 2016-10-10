using UnityEngine;
using System.Collections;

public class ArrowLogic : MonoBehaviour
{
    public float Speed = 50;

	void Update ()
    {
        transform.Rotate(Time.deltaTime * Speed, 0, 0);
	}
}
