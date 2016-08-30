using UnityEngine;
using System.Collections;

public enum ButtonType
{
    Create, Next, Reset
}

public class CameraManager : MonoBehaviour
{
    public ButtonType ButtonType;
    public GameObject CoffeeCup;
    public CoffeeCupType NewCoffeeCupType;
    public Vector3 CameraPos;
    public Vector3 CoffeeCupPos;

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

    void OnMouseDown()
    {
        switch (ButtonType)
        {
            case ButtonType.Create:
                //create new cup
                MainCamera.TargetPosition = CameraPos;
                CM.SelectedCoffee = Instantiate(CoffeeCup, CoffeeCupPos, Quaternion.identity) as GameObject;
                CM.SelectedCoffee.name = "CoffeeCup";
                CM.SelectedCoffee.GetComponent<CoffeeCupBehavior>().CupType = NewCoffeeCupType;
                CoffeeBehaviourSetup.SetCoffeeCup(ref CM.SelectedCoffee);
                break;

            case ButtonType.Next:
                if (CM.SelectedCoffee)
                {
                    //move cup to new position
                    CM.SelectedCoffee.transform.position = CoffeeCupPos;
                    MainCamera.TargetPosition = CameraPos;
                }
                break;

            case ButtonType.Reset:
                //when the player clicks the reset icon
                GameObject.Find("ResetManager").GetComponent<ResetManager>().Reset();
                break;

            default:
                break;
        }
    }
}
