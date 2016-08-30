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

public enum CoffeeDropType
{
    CoffeeDrop1,
    CoffeeDrop2
}

public enum WaterMilkType
{
    None, HotWater, IcedWater, HotMilk, IcedMilk
}

//the static class that sets behaviours of the coffee cup
public static class CoffeeBehaviourSetup
{
    //sets the coffeecup model
    public static void SetCoffeeCup(ref GameObject cup)
    {
        switch(cup.GetComponent<CoffeeCupBehavior>().CupType)
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

    //sets the material (or mesh) of the coffee
    public static void SetCoffee(ref GameObject cup)
    {
        switch(cup.GetComponent<CoffeeCupBehavior>().DropType)
        {
            case CoffeeDropType.CoffeeDrop1:
                cup.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/Coffee");
                break;
            case CoffeeDropType.CoffeeDrop2:
                cup.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/Coffee2");
                break;
            default:
                break;
        }

    }
}

