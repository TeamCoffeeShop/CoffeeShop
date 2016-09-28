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

    void Awake()
    {
        MainCamera = Camera.main.GetComponent<CameraLogic>();
    }

    public void ActivateAction (CamMType action, CoffeeCupType cuptype)
    {
        switch (action)
        {
            case CamMType.create:
                //create new cup
                MainCamera.TargetPosition = GetCameraPos(step);
                MinigameManager.Get.CoffeeManager.SelectedCoffee = CoffeeBehaviourSetup.SetCoffeeCup(cuptype);
                MinigameManager.Get.CoffeeManager.SelectedCoffee.transform.position = GetCoffeeCupPos(step);
                ++step;
                break;
            case CamMType.next:
                if (MinigameManager.Get.CoffeeManager.SelectedCoffee)
                {
                    //move cup to new position
                    MinigameManager.Get.CoffeeManager.SelectedCoffee.transform.position = GetCoffeeCupPos(step);
                    MainCamera.TargetPosition = GetCameraPos(step);
                    ++step;
                }
                break;
            case CamMType.reset:
                //when the player clicks the reset icon
                MinigameManager.Get.ResetManager.Reset();
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
                return new Vector3(-5, 55, -5);
            case 1:
                return new Vector3(5, 55, -5);
            case 2:
                return new Vector3(17, 55, -5);
        }

        return new Vector3();
    }

    Vector3 GetCoffeeCupPos (int step)
    {
        switch (step)
        {
            case 0:
                return new Vector3(-2.5f, 50.9f, 3.14f);
            case 1:
                return new Vector3(5, 50.147f, 3.14f);
            case 2:
                return new Vector3(17, 50.147f, 3.14f);
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
