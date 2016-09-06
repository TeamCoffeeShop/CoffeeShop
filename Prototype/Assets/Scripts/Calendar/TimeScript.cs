using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour {

    public GameObject time;
    GameObject timetxtobj;

    // Use this for initialization
    void Start()
    {
        time = GameObject.Find("TimeOfDay");
        timetxtobj = GameObject.Find("time");
    }

    // Update is called once per frame
    void Update()
    {
        if(time)
        {
            int currentHour = (int)time.GetComponent<TimeOfDay>().currentHour;
            int currentMin = (int)time.GetComponent<TimeOfDay>().currentMin;
            timetxtobj.GetComponent<Text>().text = currentHour + ":" + currentMin;
        }
    }
}
