using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Calendar
{
    public int month;
    public int day;
}

public class TimeOfDay : MonoBehaviour {

    //Visual clock and calendar
    public Transform hourHand;
    public Transform minuteHand;

    public Text MonthText;
    public Text DayText;

    //Set time , day and month
    public float currentHour;
    public float currentMin;

    public int month;
    public int day;

    public Calendar calendar = new Calendar();

    public int CalculateSceneLevel;

    //The number of degrees per hour
    private float hoursToDegrees = 360.0f / 12.0f;
    //The number of degrees per minute
    private float minToDegrees = 360.0f / 60.0f;

    //How long is a full day going to be in-game
    public float secondInFullDay;


    //This is what we use to set at which time our game starts 
    [Range(0, 1)]
    public float currentTimeOfDay = 0.0f;

    //This is so we can control the speed of our time of day
    private float timeMultiplier = 1.0f;

	// Use this for initialization
	void Start ()
    {
        if(PlayerPrefs.GetInt("Month") == 0)
        {
            calendar.month = month;
        }
        else
        {
            calendar.month = PlayerPrefs.GetInt("Month");
        }

        if(PlayerPrefs.GetInt("Day") == 0)
        {
            calendar.day = day;
        }
        else
        {
            calendar.day = PlayerPrefs.GetInt("Day");
        }

        MonthText.text = calendar.month + " Month";
        DayText.text = calendar.day + " Day";
    }
	
	// Update is called once per frame
	void Update () {

        currentTimeOfDay += (Time.deltaTime / secondInFullDay) * timeMultiplier;

        if (calendar.day > 30)
        {
            calendar.day = 1;
            calendar.month += 1;
            PlayerPrefs.SetInt("Month", calendar.month);
        }

        if (calendar.month > 12)
        {
            calendar.month = 1;
            PlayerPrefs.SetInt("Month", calendar.month);
        }

        MonthText.text = calendar.month + " Month";
        DayText.text = calendar.day + " Day";

        //restart our time of day to 0
        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
            calendar.day = calendar.day + 1;
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("Month", calendar.month);
            PlayerPrefs.SetInt("Day", calendar.day);
            SceneManager.LoadScene(CalculateSceneLevel);
        }

        currentHour = 24.0f * currentTimeOfDay;
        currentMin = 60 * (currentHour - Mathf.Floor(currentHour));

        hourHand.localRotation = Quaternion.Euler(0, 0, -currentHour * hoursToDegrees);
        minuteHand.localRotation = Quaternion.Euler(0, 0, -currentMin * minToDegrees);

	}
}
