using UnityEngine;
using System.Collections;

public class DialogueCameraLogic : MonoBehaviour
{
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
        GetComponent<Camera>().depth = Camera.main.depth + 1;
	}
}
