using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour {

    public GameObject NPC1;
    public GameObject NPC2;
    public GameObject NPC3;

    public int narrationLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (gameObject.name == "NPC1" || gameObject.name == "NPC2" || gameObject.name == "NPC3")
        {
            SceneManager.LoadScene(narrationLevel);
        }

    }
}
