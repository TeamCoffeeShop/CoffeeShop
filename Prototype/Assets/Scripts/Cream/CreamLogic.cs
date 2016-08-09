using UnityEngine;
using System.Collections;

public class CreamLogic : MonoBehaviour
{
    public bool InsideDrawLine = false;
   
    void OnTriggerEnter(Collider Other)
    {
        if(Other.gameObject.tag == "CreamingArea")
            InsideDrawLine = true;
    }

    void OnTriggerExit(Collider Other)
    {
        if (Other.gameObject.tag == "CreamingArea")
            InsideDrawLine = false;
    }

}
