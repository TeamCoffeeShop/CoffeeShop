using UnityEngine;
using System.Collections;

public class TimeOfDay : MonoBehaviour {

    public Transform hourHand;
    public Transform minuteHand;

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
        
	}
	
	// Update is called once per frame
	void Update () {

        currentTimeOfDay += (Time.deltaTime / secondInFullDay) * timeMultiplier;

        //restart our time of day to 0
        if (currentTimeOfDay >= 1)
            currentTimeOfDay = 0;

        float currentHour = 24.0f * currentTimeOfDay;
        float currentMin = 60 * (currentHour - Mathf.Floor(currentHour));

        hourHand.localRotation = Quaternion.Euler(0, 0, -currentHour * hoursToDegrees);
        minuteHand.localRotation = Quaternion.Euler(0, 0, -currentMin * minToDegrees);
	}
}
