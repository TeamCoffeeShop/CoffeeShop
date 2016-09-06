using UnityEngine;
using System.Collections;

public enum CamMType
{
    none, create, next, reset
}

public class CameraManager : MonoBehaviour
{
    public GameObject CoffeeCup;
    public int step;
    public GameObject NextButton;

    CameraLogic MainCamera;
    Minigame_CoffeeManager CM;

    void Awake()
    {
        CM = GameObject.Find("Manager").transform.Find("CoffeeManager").GetComponent<Minigame_CoffeeManager>();

        //if none is selected, load default cup
        if(CoffeeCup == null)
           CoffeeCup = Resources.Load<GameObject>("Prefab/CoffeeCup");

        MainCamera = GameObject.Find("Main Camera").GetComponent<CameraLogic>();
    }

    public void ActivateAction (CamMType action, CoffeeCupType cuptype)
    {
        switch (action)
        {
            case CamMType.create:
                //create new cup
                MainCamera.TargetPosition = GetCameraPos(step);
                CM.SelectedCoffee = Instantiate(CoffeeCup, GetCoffeeCupPos(step), Quaternion.identity) as GameObject;
                CM.SelectedCoffee.name = "CoffeeCup";
                CM.SelectedCoffee.GetComponent<CoffeeCupBehavior>().CupType = cuptype;
                CoffeeBehaviourSetup.SetCoffeeCup(ref CM.SelectedCoffee);
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
                return new Vector3(3, 13, -10);
            case 1:
                return new Vector3(25, 13, -10);
            case 2:
                return new Vector3(47, 13, -10);
        }

        return new Vector3();
    }

    Vector3 GetCoffeeCupPos (int step)
    {
        switch (step)
        {
            case 0:
                return new Vector3(3, 1, 1);
            case 1:
                return new Vector3(25, 1, 1);
            case 2:
                return new Vector3(47, 1, 1);
        }
            
        return new Vector3();
    }

    void ButtonCheck ()
    {
        if(NextButton)
            if (step > 2 || step < 1)
                NextButton.SetActive(false);
            else
                NextButton.SetActive(true);
    }
}
