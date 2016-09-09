using UnityEngine;
using System.Collections;

public class MainGameManager : MonoBehaviour {

    public bool OnDialogue;
    public NPCDialogue dialoguemanager;
    public GameObject MinigameButton;

	// Use this for initialization
	void Start () {

        OnDialogue = true;

	}
	
	// Update is called once per frame
	void Update () {

        if (OnDialogue)
        {
            MinigameButton.SetActive(false);
            dialoguemanager.GetComponentInChildren<Canvas>().enabled = true;
            Time.timeScale = 0.0f;
        }
        else
        {
            MinigameButton.SetActive(false);
            dialoguemanager.GetComponentInChildren<Canvas>().enabled = false;
            Time.timeScale = 1.0f;
        }
	}
}
