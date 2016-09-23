using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiaryScript : MonoBehaviour
{
    GameObject textobj;

    // Use this for initialization
    void Start()
    {
        textobj = GameObject.Find("Time");
    }

    // Update is called once per frame
    void Update()
    {
        if (MainGameManager.Get.TimeOfDay)
        {
            int currentMonth = (int)MainGameManager.Get.TimeOfDay.calendar.month;
            int currentDay = (int)MainGameManager.Get.TimeOfDay.calendar.day;
            int currentHour = (int)MainGameManager.Get.TimeOfDay.currentHour;
            int currentMin = (int)MainGameManager.Get.TimeOfDay.currentMin;
            textobj.GetComponent<Text>().text = "Date  " + currentMonth + "/" + currentDay + "  Time  " + currentHour + ":" + currentMin;
        }
    }
}
