using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiaryManager : MonoBehaviour
{
    //Public GameObjects
    public GameObject StatusPanel;
    public GameObject CalendarPanel;
    public GameObject NPCPanel;
    public GameObject DateInformation;
    public GameObject DateInformationButton;
    public GameObject NPCInformation;
    public GameObject NPCInformationButton;

    //Public Images
    private Image StatusPanelImage;
    private Image CalendarPanelImage;
    private Image NPCPanelImage;
    private Image DateInformationImage;
    private Image DateInformationButtonImage;
    private Image NPCInformationImage;
    private Image NPCInformationButtonImage;

    public bool StatusBool = false;
    bool CalendarBool = false;
    bool CalendarDetailBool = false;
    bool NPCBool = false;
    bool NPCDetailBool = false;

    GameObject[] Dates;
    GameObject[] NPCs;
    GameObject Times;

    // Use this for initialization
    void Start ()
    {
        Dates = GameObject.FindGameObjectsWithTag("Date");
        Times = GameObject.FindGameObjectWithTag("Time");
        NPCs = GameObject.FindGameObjectsWithTag("NPCList");

        //Initial Setting
        StatusBool = true;
        CalendarBool = false;
        NPCBool = false;
        CalendarDetailBool = false;
        NPCDetailBool = false;
        
        //Panel for Each Section
        StatusPanelImage = StatusPanel.GetComponent<Image>();
        CalendarPanelImage = CalendarPanel.GetComponent<Image>();
        NPCPanelImage = NPCPanel.GetComponent<Image>();

        //Date Detail Screen
        DateInformationImage = DateInformation.GetComponent<Image>();
        DateInformationButtonImage = DateInformationButton.GetComponent<Image>();
        DateInformationImage.enabled = false;
        DateInformationButtonImage.enabled = false;

        //NPC Detail Screen
        NPCInformationImage = NPCInformation.GetComponent<Image>(); ;
        NPCInformationButtonImage = NPCInformationButton.GetComponent<Image>();
        NPCInformationImage.enabled = false;
        NPCInformationButtonImage.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        TabCheck();
        CalendarStatusCheck();
        NPCStatusCheck();     
    }

    //Exit Diary Level
    public void BackToLevel(int level)
    {
        if (MainGameManager.Get.TimeOfDay.enabled == false)
        {
            MainGameManager.Get.TimeOfDay.enabled = true;
        }
        SceneManager.LoadScene(level);
    }

    public void Status ()
    {
        StatusBool = true;
        CalendarBool = false;
        CalendarDetailBool = false;        
        NPCBool = false;
        NPCDetailBool = false;
    }

    public void Calendar ()
    {
        StatusBool = false;
        CalendarBool = true;
        CalendarDetailBool = false;        
        NPCBool = false;
        NPCDetailBool = false;
    }

    public void NPC ()
    {
        StatusBool = false;
        CalendarBool = false;
        CalendarDetailBool = false;        
        NPCBool = true;
        NPCDetailBool = false;
    }

    public void DateTouch()
    {
        CalendarDetailBool = !CalendarDetailBool;
    }

    public void NPCTouch()
    {
        NPCDetailBool = !NPCDetailBool;
    }

    void CalendarStatusCheck()
    {
        if (CalendarBool)
        {
            Times.GetComponent<Text>().enabled = true;
            if (CalendarDetailBool)
            {
                foreach (GameObject Date in Dates)
                {
                    Date.GetComponent<Button>().enabled = false;
                }
                DateInformationImage.enabled = true;
                DateInformationButtonImage.enabled = true;
            }
            else
            {
                foreach (GameObject Date in Dates)
                {
                    Date.GetComponent<Image>().enabled = true;
                    Date.GetComponent<Button>().enabled = true;
                }
                DateInformationImage.enabled = false;
                DateInformationButtonImage.enabled = false;
            }
        }
        else
        {
            Times.GetComponent<Text>().enabled = false;
            foreach (GameObject Date in Dates)
            {
                Date.GetComponent<Image>().enabled = false;
                Date.GetComponent<Button>().enabled = false;
            }
            DateInformationImage.enabled = false;
            DateInformationButtonImage.enabled = false;
        }
    }

    void NPCStatusCheck()
    {
        if (NPCBool)
        {
            if (NPCDetailBool)
            {
                foreach (GameObject NPC in NPCs)
                {
                    NPC.GetComponent<Button>().enabled = false;
                }
                NPCInformationImage.enabled = true;
                NPCInformationButtonImage.enabled = true;
            }
            else
            {
                foreach (GameObject NPC in NPCs)
                {
                    NPC.GetComponent<Image>().enabled = true;
                    NPC.GetComponent<Button>().enabled = true;
                }
                NPCInformationImage.enabled = false;
                NPCInformationButtonImage.enabled = false;
            }
        }
        else
        {
            foreach (GameObject NPC in NPCs)
            {
                NPC.GetComponent<Image>().enabled = false;
                NPC.GetComponent<Button>().enabled = false;
            }
            NPCInformationImage.enabled = false;
            NPCInformationButtonImage.enabled = false;
        }
    }

    void TabCheck()
    {
        if (StatusBool)
        {
            StatusPanelImage.enabled = true;
        }
        else
        {
            StatusPanelImage.enabled = false;
        }

        if (CalendarBool)
        {
            CalendarPanelImage.enabled = true;
        }
        else
        {
            CalendarPanelImage.enabled = false;
        }

        if (NPCBool)
        {
            NPCPanelImage.enabled = true;
        }
        else
        {
            NPCPanelImage.enabled = false;
        }
    }    
}
