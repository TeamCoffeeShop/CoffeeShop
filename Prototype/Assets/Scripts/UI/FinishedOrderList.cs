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
            order.transform.SetParent(FinishedOrders, false);

            //set cup details here
            order.type = type;

            //distinguish image by the droptype
            CoffeeOrderSetup.SetOrder(order);

            //set Transform (to not stack in one place)
            PlaceOrder(order, cupPosition);
        }
    }

    void PlaceOrder (OrderLogic order, Vector3 cupPosition)
    {
        //set position to where coffee was saved
         order.GetComponent<RectTransform>().position = UIEffect.WorldToCanvasPosition(gameObject.GetComponent<RectTransform>(), MinigameManager.Get.MakeOrderCamera.GetComponent<Camera>(),cupPosition);
    }

    public void DeleteOrder (int ChildNumber)
    {
        OrderLogic order = FinishedOrders.GetChild(ChildNumber).GetComponent<OrderLogic>();
        DestroyObject(order.gameObject);
    }

    float CalculatePosition (RectTransform rt)
    {
        int count = FinishedOrders.transform.childCount;
        //RectTransform rt = order.GetComponent<RectTransform>();

        //float gap = CalculateGap(rt);

        //rt.Translate(xPos, 0, 0);
        //xPos += gap;
        //return (rt.localToWorldMatrix * rt.sizeDelta).x + 20;
        return (float)count;
    }
}