using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class Scenes 
{
    public const int NarratorLevel = 0;
    public const int MenuScreen = 1;
    public const int MainLevel = 2;
    public const int Minigame = 3;
}

public enum CoffeeCupType
{
    CoffeeCup1,
    CoffeeCup2,
    CoffeeCup3
}

public enum CoffeeDropType
{
    None,
    CoffeeDrop1,
    CoffeeDrop2
}

public enum WaterMilkType
{
    None, HotWater, IcedWater, HotMilk, IcedMilk
}

public enum OrderType
{
    None, HotAmericano, IceAmericano
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
                cup.transform.localScale = new Vector3(1.5f, 0.6f, 1.5f) * 100;
                break;
            case CoffeeCupType.CoffeeCup2:
                cup.transform.localScale = new Vector3(0.8f, 1.2f, 0.8f) * 100;
                break;
            case CoffeeCupType.CoffeeCup3:
                cup.transform.localScale = new Vector3(1,1,1) * 100;
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

//the static class that sets behaviours of the coffee order
public static class CoffeeOrderSetup
{
    //sets the image for the order
    public static void SetOrder(ref GameObject cupImage, OrderType order)
    {
        switch (order)
        {
            case OrderType.HotAmericano:
                cupImage.GetComponent<Image>().color = new Color(116 / 255f, 60 / 255f, 0);
                //Debug.Log("Hot Americano");
                break;
            case OrderType.IceAmericano:
                cupImage.GetComponent<Image>().color = new Color(206 / 255f, 164 / 255f, 114 / 255f);
                //Debug.Log("Ice Americano");
                break;
            default:
                break;
        }
    }

    //checks the coffeeordertype
    public static OrderType DistinguishCreatedMenu (ref GameObject cup)
    {
        CoffeeCupBehavior coffee = cup.GetComponent<CoffeeCupBehavior>();

        if (coffee == null)
        {
            Debug.Log("Error! Coffee is not found!");
        }
        //distinguish the menu
        else if (coffee.DropType == CoffeeDropType.CoffeeDrop1)
        {
            if(coffee.WaterMilkType == WaterMilkType.HotWater)
            {
                if(coffee.WaterMilkLevel >= 70 && coffee.WaterMilkLevel < 100)
                {
                    //Hot Americano
                    //coffeedrop1 + hot water 양 70%~100%
                    return OrderType.HotAmericano;
                }
            }
            else if (coffee.WaterMilkType == WaterMilkType.IcedWater)
            {
                if (coffee.WaterMilkLevel >= 70 && coffee.WaterMilkLevel < 100)
                {
                    //Ice Americano
                    //coffeedrop1 + iced water 양 70% ~ 100%
                    return OrderType.IceAmericano;
                }
            }
        }
        else if (coffee.DropType == CoffeeDropType.CoffeeDrop2)
        {

        }

        return OrderType.None;
    }

    public static float PriceTagForMenu (OrderType order)
    {
        switch (order)
        {
            case OrderType.HotAmericano:
                return 500;
            case OrderType.IceAmericano:
                return 700;
        }

        return -10;
    }
}