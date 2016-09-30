using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    GameObject timetxtobj;

    // Use this for initialization
    void Start()
    {
        //delete this if this wasn't from main menu
        if (MainGameManager.Get == null)
            DestroyObject(gameObject);

        timetxtobj = GameObject.Find("time");
    }

    // Update is called once per frame
    void Update()
    {
        if (MainGameManager.Get.TimeOfDay)
        {
            int currentHour = (int)MainGameManager.Get.TimeOfDay.currentHour;
            int currentMin = (int)MainGameManager.Get.TimeOfDay.currentMin;

            timetxtobj.GetComponent<Text>().text = currentHour.ToString("D2") + "  " + currentMin.ToString("D2");
        }
    }
}
