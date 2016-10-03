using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiaryManager : MonoBehaviour
{
    public DiarySelection StatusPanel;
    public DiarySelection CalendarPanel;
    public DiarySelection NPCPanel;
    public GameObject DateInformation;
    public GameObject NPCInformation;

    GameObject[] DateDetailButton;
    GameObject[] NPCDetailButton;

    public void TurnOn ()
    {
        InGameTime.SetTimeScale(0);
        StatusPanel.On();
        CalendarPanel.Off();
        NPCPanel.Off();  
        gameObject.SetActive(true);
    }

    public void TurnOff ()
    {
        InGameTime.SetTimeScale(1);
        gameObject.SetActive(false);
    }

    //diary section selection
    public void SelectSection(int WhatSection)
    {
        DateInformation.GetComponent<DiarySelection>().Off();
        NPCInformation.GetComponent<DiarySelection>().Off();

        StatusPanel.Off();
        CalendarPanel.Off();
        NPCPanel.Off();
        switch (WhatSection)
        {
            case 1:
                StatusPanel.On();
                break;
            case 2:
                CalendarPanel.On();
                break;
            case 3:
                NPCPanel.On();
                break;
        }        
    }

    public void DateInfo(bool Status)
    {
        DateDetailButton = GameObject.FindGameObjectsWithTag("DateDetail");

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
        NPCDetailButton = GameObject.FindGameObjectsWithTag("NPCDetail");

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
}
