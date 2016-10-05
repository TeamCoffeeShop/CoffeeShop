using UnityEngine;
using System.Collections;

public class SphereScript : MonoBehaviour {
    //bool type for rotation check
    public bool CheckRotation;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        CheckRotation = true;
    }

    void OnMouseUp()
    {
        CheckRotation = false;
    }
}
