using UnityEngine;
using System.Collections;

public static class Scenes 
{
    public const int NarratorLevel = 0;
    public const int MenuScreen = 1;
    public const int MainLevel = 2;
    public const int Minigame = 3;
}

public class Coffee
{
    public string coffeeName;
    public CoffeeCup coffeecup;
    //public CoffeeBean coffeebean;
    public WhipCream whipcream;
    public Syrup syrup;
}

public enum CoffeeCupType
{
    CoffeeCup1,
    CoffeeCup2,
    CoffeeCup3
}

//the static class that sets behaviours of the coffee cup
public static class CoffeeBehaviourSetup
{
    public static void SetCoffeeCup(ref GameObject cup)
    {
        switch(cup.GetComponent<CoffeeCupBehavior>().type)
        {
            case CoffeeCupType.CoffeeCup1:
                cup.transform.localScale = new Vector3(1.5f, 0.6f, 1.5f);
                break;
            case CoffeeCupType.CoffeeCup2:
                cup.transform.localScale = new Vector3(0.8f, 1.2f, 0.8f);
                break;
            case CoffeeCupType.CoffeeCup3:
                cup.transform.localScale = new Vector3(1,1,1);
                break;
            default:
                break;
        }
    }
}

