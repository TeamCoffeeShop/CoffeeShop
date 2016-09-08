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
    }

    void OnTriggerStay2D(Collider2D orderUI)
    {
        if (orderUI.tag == "CompletedOrder")
        {
            if(Input.GetMouseButtonUp(0))
            {
                //check if the order is correct one
                if (customer.GetComponent<Customer>().data.order == orderUI.GetComponent<OrderLogic>().originalCup.DistinguishedMenuName)
                {
                    //if correct, give correct respond (ex. customer leaving the cafe, paying, etc...)
                    FinishOrder(orderUI.gameObject);
                }
            }
        }
    }

    void FinishOrder (GameObject orderUI)
    {
        //increase money
        Player.AddMoneyGradually(CoffeeOrderSetup.PriceTagForMenu(customer.GetComponent<Customer>().data.order));
        Player.AddXP(CoffeeOrderSetup.XPForMenu(customer.GetComponent<Customer>().data.order));



        //delete coffee
        OrderLogic logic = orderUI.GetComponent<OrderLogic>();
        if (logic)
            logic.OrderManager.DeleteOrder(logic.ChildNumber);
        DestroyObject(logic.originalCup.gameObject);
        DestroyObject(orderUI);

        //delete customer
        DestroyObject(customer.gameObject);

        //delete customer UI
        DestroyObject(SpawnBar);
        DestroyObject(gameObject);
        Cursor.visible = true;
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
            // WORLD TO CANVAS CODE ///////////////////////////////////////////////////////////////////////////////////
            RectTransform rt = GetComponent<RectTransform>();

            //offset
            Vector3 newPos = customer.transform.position + new Vector3(0, 10, 0);

            rt.position = rt.worldToLocalMatrix * newPos;

            RectTransform CanvasRt = transform.parent.GetComponent<RectTransform>();

            Vector2 vPos = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToViewportPoint(newPos);
            Vector2 result = new Vector2(
            ((vPos.x * CanvasRt.sizeDelta.x) - (CanvasRt.sizeDelta.x * 0.5f)),
            ((vPos.y * CanvasRt.sizeDelta.y) - (CanvasRt.sizeDelta.y * 0.5f)));

            rt.anchoredPosition = result;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            //custom movement
            //rt.Translate(0, 0, 0);
        }
    }
}
