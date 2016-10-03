using UnityEngine;
using System.Collections;

public class MainGameManager : MonoBehaviour
{
    //public link
    public static MainGameManager Get;

    //Canvas
    public CalculateScene Canvas_TimeOfDay;
    public DiaryManager Canvas_Diary;
    public bool OnDialogue = true;

    //shortcuts
    public GameObject NPCManager;
    public GameObject UI;
    public DecoEditUI DecoEditUI;
    public GameObject MinigameButton;
    public GameObject NPC1;
    public LookAt maincamera;

    //shortcut permenant
    public int Scene;
    public TimeOfDay TimeOfDay;
    public FinishedOrderList OrderHUD;
    public PlayerManager playerManager;
    public FloorGridLogic Floor;
    public DialogueManager DialogueManager;
    public GameObject dialoguecamera;

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
            UI = GameObject.Find("[Canvas] UI");
            DecoEditUI = UI.transform.FindChild("DecoEditUI").GetComponent<DecoEditUI>();
            MinigameButton = UI.transform.FindChild("MiniGame").gameObject;
            NPC1 = GameObject.Find("NPC_HeadTilt");
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
                //DialogueManager.Canvas.SetActive(true);
                //NPC1.SetActive(true);
            }
            else
            {
                MinigameButton.SetActive(true);
                //DialogueManager.Canvas.SetActive(false);
                //NPC1.SetActive(false);
            }
        }
	}
}
