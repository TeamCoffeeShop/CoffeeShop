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

    public GameObject timeofday;

    private int month;
    private int day;

    private float currentTime;

    // Use this for initialization
    void Start () {
        timeofday = GameObject.Find("TimeOfDay");
        currentTime = timeofday.GetComponent<TimeOfDay>().currentTimeOfDay;

        month = timeofday.GetComponent<TimeOfDay>().calendar.month;
        day = timeofday.GetComponent<TimeOfDay>().calendar.day;

        MonthText.text = "Month : "+ month ;
        DayText.text = "Day : " + day;
    }
	
	// Update is called once per frame
	void Update () {

        month = timeofday.GetComponent<TimeOfDay>().calendar.month;
        day = timeofday.GetComponent<TimeOfDay>().calendar.day;

        MonthText.text = "Month : " + month;
        DayText.text = "Day : " + day;
    }
}
