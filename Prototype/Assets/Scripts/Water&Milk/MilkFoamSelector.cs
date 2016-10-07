using UnityEngine;
using System.Collections;

public class MilkFoamSelector : MonoBehaviour {

    //Water & Milk Instantiator
    private WaterMilkInstantiator Instantiator;

    // Use this for initialization
    void Start () {
        Instantiator = GameObject.Find("Instantiator").GetComponent<WaterMilkInstantiator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        transform.Translate(new Vector3(0, 0.1f, 0));
    }

    void OnMouseUp()
    {
        transform.Translate(new Vector3(0, -0.1f, 0));
        if (Instantiator.MilkFoam)
        {
            Instantiator.MilkFoam = false;
            Debug.Log("Milk Foam DeSelected");
        }
        else
        {
            Instantiator.MilkFoam = true;
            Debug.Log("Milk Foam Selected");
        }
    }
}
