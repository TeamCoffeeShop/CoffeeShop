using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class OrderingBallonLogic : MonoBehaviour
{
    public Transform customer;
    public GameObject SpawnBar;

    PlayerManager Player;
    int CurrentScene;

    void Start ()
    {
        CurrentScene = Scenes.asInt(SceneManager.GetActiveScene());
        Visibility();

        GameObject player = GameObject.Find("Player");
        if(player)
            Player = player.GetComponent<PlayerManager>();

        UpdatePosition();
    }

    public void OnLevelWasLoaded(int level)
    {
        CurrentScene = level;
        Visibility();

        GameObject player = GameObject.Find("Player");
        if (player)
            Player = player.GetComponent<PlayerManager>();
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
            Player.AddXP(xp);
            //popup
            Text popup = UIEffect.CPopUp(xp, transform.position).GetComponent<Text>();
            UIEffect.SetPopUpBehavior(popup, PopupType.xp);

            //increase money
            Player.AddMoneyGradually(price);
            //popup
            popup = UIEffect.CPopUp(price, transform.position + new Vector3(0, GetComponent<RectTransform>().sizeDelta.y + 10, 0)).GetComponent<Text>();
            UIEffect.SetPopUpBehavior(popup, PopupType.gold);

            //delete coffee
            OrderLogic logic = orderUI.GetComponent<OrderLogic>();
            if (logic)
                MainGameManager.Get.OrderHUD.DeleteOrder(logic.ChildNumber);
            DestroyObject(logic.originalCup.gameObject);
            DestroyObject(orderUI);

            DeleteCustomer();

            Cursor.visible = true;
        }
    }

    public void DeleteCustomer ()
    {
        //delete customer
        DestroyObject(customer.gameObject);

        //delete customer UI
        DestroyObject(SpawnBar);
        DestroyObject(gameObject);
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
            UIEffect.WorldToCanvas(transform.parent.gameObject, customer.transform.position + new Vector3(0, 17, 0), GetComponent<RectTransform>());
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (Colliding)
            {
                //if correct, give correct respond (ex. customer leaving the cafe, paying, etc...)
                FinishOrder(OrderUI.gameObject);
                Colliding = false;
            }
        }
    }
}
