using UnityEngine;
using System.Collections;

public class MainGameManager : MonoBehaviour {

    public bool OnDialogue;
    public NPCDialogue dialoguemanager;
    public GameObject MinigameButton;

    public GameObject NPC1;
    public GameObject NPC2;
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
            NPC1.SetActive(true);
            NPC2.SetActive(true);
            //Time.timeScale = 0.0f;
        }
        else
        {
            MinigameButton.SetActive(true);
            dialoguemanager.GetComponentInChildren<Canvas>().enabled = false;
            NPC1.SetActive(false);
            NPC2.SetActive(false);
            //Time.timeScale = 1.0f;
        }
	}
}
