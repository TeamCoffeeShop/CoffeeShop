using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum DayOrNight
{
    Day, Night
}

public class TimeOfDay : MonoBehaviour
{
    public DayOrNight currentStatus;
    //Set time , day and month
    public float currentHour;
    public float currentMin;

    //How long is a full day going to be in-game
    public float secondInFullDay;

    public int month;
    public int day;

    public int CalculateSceneLevel;

    public Calendar calendar = new Calendar();

    //This is what we use to set at which time our game starts 
    [Range(0, 1)]
    public float currentTimeOfDay = 0.3f;

    //This is so we can control the speed of our time of day
    private float timeMultiplier = 1.0f;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("Month") == 0)
        {
            calendar.month = month;
        }
        else
        {
            calendar.month = PlayerPrefs.GetInt("Month");
        }

        if (PlayerPrefs.GetInt("Day") == 0)
        {
            calendar.day = day;
        }
        else
        {
            calendar.day = PlayerPrefs.GetInt("Day");
        }
        currentTimeOfDay += 0.250f; //06:00
        //currentTimeOfDay += 0.917f; // 22:00
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeOfDay += (InGameTime.deltaTime / secondInFullDay) * timeMultiplier;

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

        currentHour = 24.0f * currentTimeOfDay;
        currentMin = 60 * (currentHour - Mathf.Floor(currentHour));

        //restart our time of day to 0
        if (currentTimeOfDay >= 0.917f)
        {
            currentTimeOfDay = 0.250f;
            calendar.day = calendar.day + 1;

            //reset customers & cups
            Transform HUD = MainGameManager.Get.Canvas_OrderHUD.transform;

            int size = HUD.FindChild("Finished Orders").transform.childCount;
            for (int i = 0; i < size; ++i)
                HUD.GetComponent<FinishedOrderList>().DeleteOrder(i);

            MainGameManager.Get.Canvas_TimeOfDay.TurnOn();
        }

    }
}

