using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public static class Scenes 
{
    public const int MenuScreen = 0;
    public const int MainLevel = 1;
    public const int Minigame = 2;

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

                //temporary showoff! erase this after.
                if(cup.GetComponent<CoffeeCupBehavior>().CupType == CoffeeCupType.Mug)
                {
                    cup.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
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

public enum PopupType
{
    none, gold, xp, penalty
}

public static class UIEffect
{
    static GameObject Canvas;
    static GameObject Popup;

    static bool SetCanvas ()
    {
        Canvas = MainGameManager.Get.Canvas_UI.gameObject;

        return Canvas != null;
    }

    static bool SetPopup ()
    {
        Popup = Resources.Load<GameObject>("Prefab/UI/Popup");

        return Popup != null;
    }

    public static GameObject WPopUp(float number, Vector3 worldposition)
    {
        if(!Canvas)
            if(!SetCanvas())
                return null;

        if (!Popup)
            if (!SetPopup())
                return null;

        GameObject p = GameObject.Instantiate(Popup);
        p.transform.SetParent(Canvas.transform);
        p.GetComponent<Text>().text = number.ToString("N0");

        WorldToCanvas(Canvas, worldposition, p.GetComponent<RectTransform>());

        return p;
    }

    public static GameObject CPopUp(float number, Vector3 canvasposition)
    {
        if (!Canvas)
            if (!SetCanvas())
                return null;

        if (!Popup)
            if (!SetPopup())
                return null;

        GameObject p = GameObject.Instantiate(Popup);
        p.transform.SetParent(Canvas.transform);
        p.GetComponent<Text>().text = number.ToString("N0");

        p.GetComponent<RectTransform>().position = canvasposition;

        return p;
    }

    public static void WorldToCanvas (GameObject canvas, Vector3 worldposition, RectTransform rt)
    {
        RectTransform CanvasRt = canvas.GetComponent<RectTransform>();

        Vector2 vPos = Camera.main.WorldToViewportPoint(worldposition);
        Vector2 result = new Vector2(
        ((vPos.x * CanvasRt.sizeDelta.x) - (CanvasRt.sizeDelta.x * 0.5f)),
        ((vPos.y * CanvasRt.sizeDelta.y) - (CanvasRt.sizeDelta.y * 0.5f)));

        rt.anchoredPosition = result;
    }

    public static void SetPopUpBehavior(Text popup, PopupType type)
    {
        switch (type)
        {
            case PopupType.gold:
                {
                    popup.color = new Color(1, 231 / 255.0f, 15 / 255.0f);
                    string ntext = "+ $ " + popup.text;
                    popup.text = ntext;
                }
                break;
            case PopupType.xp:
                {
                    popup.color = new Color(94 / 255.0f, 163 / 255.0f, 1);
                    string ntext = "+ XP " + popup.text;
                    popup.text = ntext;
                }
                break;
            case PopupType.penalty:
                {
                    popup.color = Color.red;
                    string ntext = "- $ " + popup.text;
                    popup.text = ntext;
                }
                break;

        }
    }
}