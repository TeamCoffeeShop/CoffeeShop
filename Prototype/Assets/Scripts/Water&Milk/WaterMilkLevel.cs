﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaterMilkLevel : MonoBehaviour
{
    private WaterMilkInstantiator Instantiator;

    public GameObject WaterMilkText;
    public GameObject WaterMilkGauge;
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
            if (Instantiator.WaterMilkType == WaterMilkType.Water)
            {
                Instantiator.water.GetComponent<WaterFallingLogic>().filling = false;
                Instantiator.water = null;
            }

            if (Instantiator.WaterMilkType == WaterMilkType.Water)
            {
                Instantiator.water.GetComponent<WaterFallingLogic>().filling = false;
                Instantiator.water = null;
            }
            Debug.Log("Overflow!!");
            
        }
        
        if(WaterMilkText && WaterMilkGauge)
        {
            float pT = Level * 0.01f;
            float p = Level * 0.01f * 0.5f;
            WaterMilkText.GetComponent<Text>().text = pT.ToString("P");

            WaterMilkGauge.transform.localScale = new Vector3(0.04f, p, 1); 
        }
	}
}
