using UnityEngine;
using System.Collections;

public class MainGameManager : MonoBehaviour
{
    //public link
    public static MainGameManager Get;

    //shortcuts
    public DialogueManager dialoguemanager;
    public GameObject NPCManager;
    public GameObject UI;
    public GameObject DecoEditUI;
    public GameObject MinigameButton;
    public GameObject NPC1;
    public FloorGridLogic Floor;
    public LookAt maincamera;
    public bool OnDialogue = true;

    //shortcut permenant
    public int Scene;
    public TimeOfDay TimeOfDay;
    public FinishedOrderList OrderHUD;
    public PlayerManager playerManager;

    void Awake()
    {
        if(Get == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Get = this;
            Scene = Scenes.MainLevel;
            LoadShortcuts();
        }
        else
        {
            DestroyObject(this.gameObject);
        }
    }
	
    public void OnLevelWasLoaded (int level)
    {
        Scene = level;
        LoadShortcuts();
    }

    void LoadShortcuts ()
    {
        if(Scene == Scenes.MainLevel)
        {
            NPCManager = GameObject.Find("NPCManager");
            dialoguemanager = GameObject.Find("DialogueSystem").GetComponent<DialogueManager>();
            UI = GameObject.Find("UI");
            DecoEditUI = UI.transform.FindChild("DecoEditUI").gameObject;
            MinigameButton = UI.transform.FindChild("MiniGame").gameObject;
            NPC1 = GameObject.Find("NPC_HeadTilt");
            Floor = GameObject.Find("Structure").transform.FindChild("FloorGrid").GetComponent<FloorGridLogic>();
            maincamera = Camera.main.GetComponent<LookAt>();
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if(Scene == Scenes.MainLevel)
        {
            if (OnDialogue)
            {
                MinigameButton.SetActive(false);
                dialoguemanager.GetComponentInChildren<Canvas>().enabled = true;
                //NPC1.SetActive(true);
            }
            else
            {
                MinigameButton.SetActive(true);
                dialoguemanager.GetComponentInChildren<Canvas>().enabled = false;
                //NPC1.SetActive(false);
            }
        }
	}
}
