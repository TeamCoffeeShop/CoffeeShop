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
            //create cup order
            OrderLogic order = GameObject.Instantiate(orderDisplayButton).GetComponent<OrderLogic>();
            order.ChildNumber = FinishedOrders.childCount;
            order.transform.SetParent(FinishedOrders, false);

            //set cup order details here
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
        order.GetComponent<OrderLogic>().OriginalPosition = CalculatePosition(order, FinishedOrders.childCount - 1);
    }

    public void DeleteOrder (int ChildNumber)
    {
        //if invalid order, return
        if (ChildNumber >= FinishedOrders.childCount && ChildNumber < 0)
            return;

        OrderLogic order = FinishedOrders.GetChild(ChildNumber).GetComponent<OrderLogic>();
        DestroyObject(order.gameObject);

        //organize orders at back numbers
        for (int i = ChildNumber; i < FinishedOrders.childCount; ++i)
        {
            --FinishedOrders.GetChild(i).GetComponent<OrderLogic>().ChildNumber;
            FinishedOrders.GetChild(i).GetComponent<OrderLogic>().OriginalPosition = CalculatePosition(FinishedOrders.GetChild(i).GetComponent<RectTransform>(), i - 1);
        }
    }

    Vector3 CalculatePosition (RectTransform rt, int count)
    {
        float size = (rt.localToWorldMatrix * rt.sizeDelta).x + 20;
        return new Vector3(size * count + 20, 20, 0);
    }
}