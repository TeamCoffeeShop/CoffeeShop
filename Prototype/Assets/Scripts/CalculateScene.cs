using UnityEngine;
using System.Collections;

public class CalculateScene : MonoBehaviour {

    private GameObject gameobject;
	// Use this for initialization
	void Start () {

        gameobject = GameObject.Find("CustomerList(Clone)");
        Destroy(gameobject);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
