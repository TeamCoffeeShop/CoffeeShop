using UnityEngine;
using System.Collections;

public class WaterMilkLevel : MonoBehaviour
{
    private WaterMilkInstantiator Instantiator;

    public float Level;

    // Use this for initialization
    void Start ()
    {
        Instantiator = GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Level = (Instantiator.CurrentAmount / Instantiator.MaxAmount) * 100;
        //if the liquid is flowing over the cup, reset the progress
        //and go back to the first step
        if (Level > 100)
        {
            Debug.Log("Overflow!!");
            GameObject.Find("ResetManager").GetComponent<ResetManager>().Reset();
        }
        
	}
}
