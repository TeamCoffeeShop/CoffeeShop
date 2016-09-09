using UnityEngine;
using System.Collections;

public class CalculateScene : MonoBehaviour {

    private GameObject gameobject;
    private GameObject clockobject;

	// Use this for initialization
	void Start () {

        gameobject = GameObject.Find("CustomerList(Clone)");
        clockobject = GameObject.Find("TimeOfDay");
        Destroy(gameobject);
        Destroy(clockobject);
        PlayerPrefs.SetInt("DialogueID", 2);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
