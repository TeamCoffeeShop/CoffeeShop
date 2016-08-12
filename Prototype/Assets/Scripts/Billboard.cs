using UnityEngine;
using System.Collections;

//this class enables to make the owner always face forward to camera.
public class Billboard : MonoBehaviour
{
    public bool active = true;
    public bool onlyAtStart = false;

    void Start()
    {
        if (active)
        {
            transform.forward = -Camera.main.transform.forward;
        }
    }

    void Update()
    {
        if (active && !onlyAtStart)
        {
            transform.forward = -Camera.main.transform.forward;
        }
    }
}