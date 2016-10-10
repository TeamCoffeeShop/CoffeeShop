using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinishedOrderList : MonoBehaviour
{
    public GameObject orderDisplayButton;

    Transform FinishedOrders;

    void Start ()
    {
        FinishedOrders = transform.FindChild("Finished Orders");
    }

    public void CreateOrderInUI(Vector3 cupPosition, OrderType type)
    {
        if (type != OrderType.None)
        {
            //create cup
            OrderLogic order = GameObject.Instantiate(orderDisplayButton).GetComponent<OrderLogic>();
            order.ChildNumber = FinishedOrders.childCount;
            order.transform.SetParent(FinishedOrders, false);

            //set cup details here
            order.type = type;

            //distinguish image by the droptype
            CoffeeOrderSetup.SetOrder(order);

            //set Transform (to not stack in one place)
            PlaceOrder(order.GetComponent<RectTransform>(), cupPosition);
        }
    }

    void PlaceOrder (RectTransform order, Vector3 cupPosition)
    {
        //set position to where coffee was saved
        order.position = UIEffect.WorldToCanvasPosition(gameObject.GetComponent<RectTransform>(), Camera.main, cupPosition);
        //set originalPosition
        order.GetComponent<OrderLogic>().OriginalPosition = new Vector3(CalculatePosition(order),20,0);
    }

    public void DeleteOrder (int ChildNumber)
    {
        OrderLogic order = FinishedOrders.GetChild(ChildNumber).GetComponent<OrderLogic>();
        DestroyObject(order.gameObject);
    }

    float CalculatePosition (RectTransform rt)
    {
        int count = FinishedOrders.transform.childCount - 1;
        float size = (rt.localToWorldMatrix * rt.sizeDelta).x + 20;
        return size * count + 20;
    }
}