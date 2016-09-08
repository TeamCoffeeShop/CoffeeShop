using UnityEngine;
using System.Collections;

public enum CamMType
{
    none, create, next, reset
}

public class CameraManager : MonoBehaviour
{
    public int step;
    public GameObject NextButton;
    public GameObject WaterMilkText;
    public GameObject WaterMilkGauge;
    public GameObject WaterMilkGaugeBG;

    CameraLogic MainCamera;
    Minigame_CoffeeManager CM;

    void Awake()
    {
        CM = GameObject.Find("Manager").transform.Find("CoffeeManager").GetComponent<Minigame_CoffeeManager>();

        MainCamera = GameObject.Find("Main Camera").GetComponent<CameraLogic>();
    }

    public void ActivateAction (CamMType action, CoffeeCupType cuptype)
    {
        switch (action)
        {
            case CamMType.create:
                //create new cup
                MainCamera.TargetPosition = GetCameraPos(step);
                CM.SelectedCoffee = CoffeeBehaviourSetup.SetCoffeeCup(cuptype);
                CM.SelectedCoffee.transform.position = GetCoffeeCupPos(step);
                ++step;
                break;
            case CamMType.next:
                if (CM.SelectedCoffee)
                {
                    //move cup to new position
                    CM.SelectedCoffee.transform.position = GetCoffeeCupPos(step);
                    MainCamera.TargetPosition = GetCameraPos(step);
                    ++step;
                }
                break;
            case CamMType.reset:
                //when the player clicks the reset icon
                GameObject.Find("ResetManager").GetComponent<ResetManager>().Reset();
                step = 0;
                break;
        }
        ButtonCheck();
    }

    Vector3 GetCameraPos (int step)
    {
        switch (step)
        {
            case 0:
                return new Vector3(-5, 5, -5);
            case 1:
                return new Vector3(5, 5, -5);
            case 2:
                return new Vector3(17, 5, -5);
        }

        return new Vector3();
    }

    Vector3 GetCoffeeCupPos (int step)
    {
        switch (step)
        {
            case 0:
                return new Vector3(-2.5f, 0.9f, 3.14f);
            case 1:
                return new Vector3(5, 0.147f, 3.14f);
            case 2:
                return new Vector3(17, 0.147f, 3.14f);
        }
            
        return new Vector3();
    }

    public void ButtonCheck ()
    {
        //Next Button
        if(NextButton)
            if (step > 2 || step < 1)
                NextButton.SetActive(false);
            else
                NextButton.SetActive(true);

        //Water Milk Text
        if(WaterMilkText)
        {
            if (step != 2)
                WaterMilkText.SetActive(false);
            else
                WaterMilkText.SetActive(true);
        }

        //Water Milk Gauge
        if (WaterMilkText && WaterMilkGaugeBG)
        {
            if (step != 2)
            {
                WaterMilkGauge.SetActive(false);
                WaterMilkGaugeBG.SetActive(false);

            }
            else
            {
                WaterMilkGauge.SetActive(true);
                WaterMilkGaugeBG.SetActive(true);

            }
        }


    }
}
