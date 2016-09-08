using UnityEngine;
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
            Debug.Log("Overflow!!");
            GameObject.Find("Manager").transform.FindChild("CameraManager").GetComponent<CameraManager>().ActivateAction(CamMType.reset, CoffeeCupType.Standard);
        }
        
        if(WaterMilkText && WaterMilkGauge)
        {
            float p = Level * 0.01f;
            WaterMilkText.GetComponent<Text>().text = p.ToString("P");

            WaterMilkGauge.transform.localScale = new Vector3(1, p, 1); 
        }
	}
}
