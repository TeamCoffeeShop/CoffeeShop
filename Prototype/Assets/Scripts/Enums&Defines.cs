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
public enum HotIceType
{
    None, Hot, Ice
}
public enum WaterMilkType
{
    None, Water, Milk
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
    public static void SetOrder(OrderLogic order)
    {
        switch (order.type)
        {
            case OrderType.HotAmericano:
                order.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UI/Order_WOtext/HotAmericano_WOText");
                break;
            case OrderType.IceAmericano:
                order.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UI/Order_WOtext/ColdAmericano_WOText");
                break;
            default:
                break;
        }
    }

    public static void SetOrder(GameObject orderballon, OrderType order)
    {
        switch (order)
        {
            case OrderType.HotAmericano:
                orderballon.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UI/Order_WOtext/HotAmericano_WOText");
                break;
            case OrderType.IceAmericano:
                orderballon.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/UI/Order_WOtext/ColdAmericano_WOText");
                break;
            default:
                break;
        }
    }

    //checks the coffeeordertype
    public static OrderType DistinguishCreatedMenu(CoffeeCupBehavior cup)
    {
        //distinguish the menu
        if (cup.DropType == CoffeeDropType.CoffeeDrop1)
        {
            if (cup.WaterMilkType == WaterMilkType.Water)
            {
                if (cup.WaterMilkLevel >= 70 && cup.WaterMilkLevel < 100)
                {
                    //temporary recipe!

                    //Hot Americano
                    //coffeedrop1 + hot water 양 70%~100%
                    return OrderType.HotAmericano;
                }
            }
            //else if (coffee.WaterMilkType == WaterMilkType.IcedWater)
            //{
            //    if (coffee.WaterMilkLevel >= 70 && coffee.WaterMilkLevel < 100)
            //    {
            //        //Ice Americano
            //        //coffeedrop1 + iced water 양 70% ~ 100%
            //        return OrderType.IceAmericano;
            //    }
            //}
        }
        else if (cup.DropType == CoffeeDropType.CoffeeDrop2)
        {
            //temporary recipe!!
            return OrderType.IceAmericano;
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

        p.GetComponent<RectTransform>().position = UIEffect.WorldToCanvasPosition(Canvas.GetComponent<RectTransform>(), Camera.main, worldposition);

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

    public static Vector2 WorldToCanvasPosition(RectTransform canvas, Camera camera, Vector3 position)
    {
        //Vector position (percentage from 0 to 1) considering camera size.
        //For example (0,0) is lower left, middle is (0.5,0.5)
        Vector2 temp = camera.WorldToViewportPoint(position);

        //Calculate position considering our percentage, using our canvas size
        //So if canvas size is (1100,500), and percentage is (0.5,0.5), current value will be (550,250)
        temp.x *= canvas.sizeDelta.x;
        temp.y *= canvas.sizeDelta.y;

        //The result is ready, but, this result is correct if canvas recttransform pivot is 0,0 - left lower corner.
        //But in reality its middle (0.5,0.5) by default, so we remove the amount considering cavnas rectransform pivot.
        //We could multiply with constant 0.5, but we will actually read the value, so if custom rect transform is passed(with custom pivot) , 
        //returned value will still be correct.
        //temp.x -= canvas.sizeDelta.x * canvas.pivot.x;
        //temp.y -= canvas.sizeDelta.y * canvas.pivot.y;

        return temp;
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