using UnityEngine;
using System.Collections;

public class DialogueCameraLogic : MonoBehaviour
{
	void Start ()
    {
        GetComponent<Camera>().depth = Camera.main.depth + 1;
	}
}
