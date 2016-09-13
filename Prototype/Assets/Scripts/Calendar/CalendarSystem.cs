using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Calendar
{
    public int month;
    public int day;
}

public class CalendarSystem : MonoBehaviour {

    public Text MonthText;
    public Text DayText;

    private int month;
    private int day;

    private float currentTime;

    // Use this for initialization
    void Start () 
    {
        currentTime = MainGameManager.Get.TimeOfDay.currentTimeOfDay;

        month = MainGameManager.Get.TimeOfDay.calendar.month;
        day = MainGameManager.Get.TimeOfDay.calendar.day;

        MonthText.text = "Month : "+ month ;
        DayText.text = "Day : " + day;
    }
	
	// Update is called once per frame
	void Update () {

        month = MainGameManager.Get.TimeOfDay.calendar.month;
        day = MainGameManager.Get.TimeOfDay.calendar.day;

        MonthText.text = "Month : " + month;
        DayText.text = "Day : " + day;
    }
}
