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

    bool StatusBool = false;
    bool CalendarBool = false;
    bool CalendarDetailBool = false;
    bool NPCBool = false;

    GameObject[] Dates;
    GameObject Times;

    // Use this for initialization
    void Start ()
    {
        //All of the Single Date Icon
        Dates = GameObject.FindGameObjectsWithTag("Date");
        Times = GameObject.FindGameObjectWithTag("Time");
       
        //Initial Setting
        StatusBool = true;
        CalendarBool = false;
        NPCBool = false;
        CalendarDetailBool = false;
        
        //Panel for Each Section
        StatusPanelImage = StatusPanel.GetComponent<Image>();
        CalendarPanelImage = CalendarPanel.GetComponent<Image>();
        NPCPanelImage = NPCPanel.GetComponent<Image>();

        //Date Detail Screen
        DateInformationImage = DateInformation.GetComponent<Image>();
        DateInformationButtonImage = DateInformationButton.GetComponent<Image>();
        DateInformationImage.enabled = false;
        DateInformationButtonImage.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        TabCheck();
        CalendarStatusCheck();       
    }

    public void BackToLevel(int level)
    {
        if (MainGameManager.Get.TimeOfDay.enabled == false)
        {
            Debug.Log("a");
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
    }

    public void Calendar ()
    {
        StatusBool = false;
        CalendarBool = true;
        CalendarDetailBool = false;        
        NPCBool = false;
    }

    public void NPC ()
    {
        StatusBool = false;
        CalendarBool = false;
        CalendarDetailBool = false;        
        NPCBool = true;        
    }

    public void DateTouch()
    {
        CalendarDetailBool = !CalendarDetailBool;
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
