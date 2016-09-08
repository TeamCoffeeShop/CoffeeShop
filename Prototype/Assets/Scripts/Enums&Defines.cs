using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public static class Scenes 
{
    public const int NarratorLevel = 0;
    public const int MenuScreen = 1;
    public const int MainLevel = 2;
    public const int Minigame = 3;

    public static int asInt(Scene scene)
    {
        switch (scene.name)
        {
            case "CoffeePrototype":
                return Minigame;
            case "MainGame":
                return MainLevel;
            case "Menu":
                return MenuScreen;
            case "Narration":
                return NarratorLevel;
        }

        return 0;
    }
}

public enum CoffeeCupType
{
    Mug,
    Standard,
    Cappuccino
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
    public static GameObject SetCoffeeCup(CoffeeCupType type)
    {
        GameObject cup = null;

        switch(type)
        {
            case CoffeeCupType.Mug:
                cup = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/CoffeeCups/Mug"));
                cup.transform.Rotate(0, 180, 0);
                break;
            case CoffeeCupType.Standard:
                cup = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/CoffeeCups/Standard"));
                cup.transform.Rotate(0, 180, 0);
                break;
            case CoffeeCupType.Cappuccino:
                cup = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/CoffeeCups/Cappuccino"));
                cup.transform.Rotate(0, 180, 0);
                break;
            default:
                break;
        }

        return cup;
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
                cupImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/Americano");
                //Debug.Log("Hot Americano");
                break;
            case OrderType.IceAmericano:
                cupImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/AmericanoCld");
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

    public static float XPForMenu(OrderType order)
    {
        switch (order)
        {
            case OrderType.HotAmericano:
                return 50;
            case OrderType.IceAmericano:
                return 50;
        }

        return -10;
    }
}