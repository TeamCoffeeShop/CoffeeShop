using UnityEngine;
using System.Collections;

public class MainMenuScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("DialogueID", 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
