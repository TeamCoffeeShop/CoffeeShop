using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class OrderingBallonLogic : MonoBehaviour
{
    public Transform customer;
    public GameObject SpawnBar;

    bool OrderCheck;
    int CurrentScene;

    void Start ()
    {
        CurrentScene = Scenes.asInt(SceneManager.GetActiveScene());
        Visibility();

        UpdatePosition();
    }

    public void OnLevelWasLoaded(int level)
    {
        CurrentScene = level;
        Visibility();
    }

    bool Colliding = false;
    Collider2D OrderUI;

    void OnTriggerStay2D(Collider2D orderUI)
    {
        if (orderUI.tag == "CompletedOrder")
        {
            //check if the order is correct one
            if (customer.GetComponent<Customer>().data.order == orderUI.GetComponent<OrderLogic>().originalCup.DistinguishedMenuName)
            {
                OrderUI = orderUI;
                Colliding = true;
                OrderCheck = true;
            }
            else
            {
                OrderUI = orderUI;
                Colliding = true;
                OrderCheck = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D orderUI)
    {
        if(OrderUI)
        {
            OrderUI = null;
            Colliding = false;
        }
    }

    void FinishOrder (GameObject orderUI)
    {
        if(customer)
        {
            float price = CoffeeOrderSetup.PriceTagForMenu(customer.GetComponent<Customer>().data.order);
            float xp = CoffeeOrderSetup.XPForMenu(customer.GetComponent<Customer>().data.order);

            //increase xp
            MainGameManager.Get.playerManager.AddXP(xp);
            //popup
            Text popup = UIEffect.CPopUp(xp, transform.position).GetComponent<Text>();
            UIEffect.SetPopUpBehavior(popup, PopupType.xp);

            //increase money
            MainGameManager.Get.playerManager.AddMoneyGradually(price);
            //popup
            popup = UIEffect.CPopUp(price, transform.position + new Vector3(0, GetComponent<RectTransform>().sizeDelta.y + 10, 0)).GetComponent<Text>();
            UIEffect.SetPopUpBehavior(popup, PopupType.gold);

            //delete coffee
            OrderLogic logic = orderUI.GetComponent<OrderLogic>();
            if (logic)
                MainGameManager.Get.Canvas_OrderHUD.DeleteOrder(logic.ChildNumber);
            DestroyObject(logic.originalCup.gameObject);
            DestroyObject(orderUI);

            customer.GetComponent<CustomerLogic>().LeaveCoffeeShop();

            Cursor.visible = true;
        }
    }

    void GivePenalty(GameObject orderUI)
    {
        int xp = 5;
        int price = 200;

        if (customer)
        {
            //increase xp
            MainGameManager.Get.playerManager.SubtractXP(xp);

            //increase money
            MainGameManager.Get.playerManager.SubtractMoneyGradully(price);
            //popup
            Text popup = UIEffect.CPopUp(price, transform.position + new Vector3(0, GetComponent<RectTransform>().sizeDelta.y + 10, 0)).GetComponent<Text>();
            UIEffect.SetPopUpBehavior(popup, PopupType.penalty);

            //delete coffee
            OrderLogic logic = orderUI.GetComponent<OrderLogic>();
            if (logic)
                MainGameManager.Get.Canvas_OrderHUD.DeleteOrder(logic.ChildNumber);
            DestroyObject(logic.originalCup.gameObject);
            DestroyObject(orderUI);

            customer.GetComponent<CustomerLogic>().LeaveCoffeeShop();

            Cursor.visible = true;
        }

    }

    void Visibility ()
    {
        if (CurrentScene == Scenes.MainLevel)
        {
            GetComponent<Image>().enabled = true;
            UpdatePosition();
        }
        else
            GetComponent<Image>().enabled = false;
    }

    void UpdatePosition ()
    {
        //follow link position
        if (customer != null)
        {
            UIEffect.WorldToCanvas(transform.parent.gameObject, customer.transform.position + new Vector3(0, 23, 0), GetComponent<RectTransform>());
        }
    }

    void Update()
    {
        UpdatePosition();

        if (Input.GetMouseButtonUp(0))
        {
            if (Colliding)
            {
                if (OrderCheck)
                {
                    //if correct, give correct respond (ex. customer leaving the cafe, paying, etc...)
                    FinishOrder(OrderUI.gameObject);
                    Colliding = false;
                }
                else
                {
                    GivePenalty(OrderUI.gameObject);
                    Colliding = false;
                }

            }
        }
    }
}
