using UnityEngine;
using System.Collections;

public class ClockSystem : MonoBehaviour {

    //Visual clock and calendar
    public Transform hourHand;
    public Transform minuteHand;

    public GameObject timesystem;

    private float hour;
    private float min;

    //The number of degrees per hour
    private float hoursToDegrees = 360.0f / 12.0f;
    //The number of degrees per minute
    private float minToDegrees = 360.0f / 60.0f;

    // Use this for initialization
    void Start () {

        hour = timesystem.GetComponent<TimeOfDay>().currentHour;
        min = timesystem.GetComponent<TimeOfDay>().currentMin;
    }
	
	// Update is called once per frame
	void Update () {

        hour = timesystem.GetComponent<TimeOfDay>().currentHour;
        min = timesystem.GetComponent<TimeOfDay>().currentMin;

        hourHand.localRotation = Quaternion.Euler(0, 0, -hour * hoursToDegrees);
        minuteHand.localRotation = Quaternion.Euler(0, 0, -min * minToDegrees);

    }
}
