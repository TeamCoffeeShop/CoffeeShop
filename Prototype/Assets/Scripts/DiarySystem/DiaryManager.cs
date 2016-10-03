using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiaryManager : MonoBehaviour
{
    public GameObject StatusPanel;
    public GameObject CalendarPanel;
    public GameObject NPCPanel;
    public GameObject DateInformation;
    public GameObject NPCInformation;

    GameObject[] DateDetailButton;
    GameObject[] NPCDetailButton;

    // Use this for initialization
    void Start ()
    {
        StatusPanel.GetComponent<DiarySelection>().On();
        CalendarPanel.GetComponent<DiarySelection>().Off();
        NPCPanel.GetComponent<DiarySelection>().Off();        
    }
	
	// Update is called once per frame
	void Update ()
    {
        DateDetailButton = GameObject.FindGameObjectsWithTag("DateDetail");
        NPCDetailButton = GameObject.FindGameObjectsWithTag("NPCDetail");
    }

    //diary section selection
    public void SelectSection(int WhatSection)
    {
        DateInformation.GetComponent<DiarySelection>().Off();
        NPCInformation.GetComponent<DiarySelection>().Off();

        switch (WhatSection)
        {
            case 1:
                StatusPanel.GetComponent<DiarySelection>().On();
                CalendarPanel.GetComponent<DiarySelection>().Off();
                NPCPanel.GetComponent<DiarySelection>().Off();
                break;
            case 2:
                StatusPanel.GetComponent<DiarySelection>().Off();
                CalendarPanel.GetComponent<DiarySelection>().On();
                NPCPanel.GetComponent<DiarySelection>().Off();
                break;
            case 3:
                StatusPanel.GetComponent<DiarySelection>().Off();
                CalendarPanel.GetComponent<DiarySelection>().Off();
                NPCPanel.GetComponent<DiarySelection>().On();
                break;
        }        
    }

    public void DateInfo(bool Status)
    {
        if (Status)
        {
            DateInformation.GetComponent<DiarySelection>().On();
            foreach (GameObject Date in DateDetailButton)
            {
                Date.GetComponent<Button>().enabled = false;
            }
        }
        else
        {
            DateInformation.GetComponent<DiarySelection>().Off();
            foreach (GameObject Date in DateDetailButton)
            {
                Date.GetComponent<Button>().enabled = true;
            }
        }
    }

    public void NPCInfo(bool Status)
    {
        if (Status)
        {
            NPCInformation.GetComponent<DiarySelection>().On();
            foreach (GameObject NPC in NPCDetailButton)
            {
                NPC.GetComponent<Button>().enabled = false;
            }
        }
        else
        {
            NPCInformation.GetComponent<DiarySelection>().Off();
            foreach (GameObject NPC in NPCDetailButton)
            {
                NPC.GetComponent<Button>().enabled = true;
            }
        }
    }

    //exit diary level
    public void BackToLevel()
    {
        InGameTime.SetTimeScale(1);
        SceneManager.LoadScene(1);
    }
}
