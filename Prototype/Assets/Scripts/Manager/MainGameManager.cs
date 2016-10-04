using UnityEngine;
using System.Collections;

public class MainGameManager : MonoBehaviour
{
    //public link
    public static MainGameManager Get;

    //Canvas
    public UISystem Canvas_UI;
    public CalculateScene Canvas_TimeOfDay;
    public DiaryManager Canvas_Diary;
    public FinishedOrderList Canvas_OrderHUD;

    //camera
    public GameObject DialogueCamera;

    //shortcuts
    public CustomerSystem CustomerSystem;
    public FloorGridLogic Floor;
    public DialogueManager DialogueManager;
    public CalendarSystem CalendarSystem;
    public TimeOfDay TimeOfDay;
    public PlayerManager playerManager;
    public DecoEditUI DecoEditUI;
    public SceneChangeManager SceneChangeManager;
    public bool OnDialogue = true;

    void Awake()
    {
        Get = this;
    }
}
