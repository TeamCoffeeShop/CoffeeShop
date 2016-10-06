using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinishedOrderList : MonoBehaviour
{
    public GameObject orderDisplayButton;

    Transform orders;
    Transform OrderIcon;

    void Start ()
    {
        orders = transform.FindChild("Finished Orders");
        OrderIcon = transform.FindChild("Order Icons");
    }

    public void CreateOrdersInUI()
    {
        if (orders)
        {
            //create cups as much as childrens
            int size = orders.childCount;
            float xPos = 0;
            for (int i = 0; i < size; ++i)
            {
                //create cup
                GameObject cup = GameObject.Instantiate(orderDisplayButton);
                cup.transform.SetParent(OrderIcon, false);

                //set cup details here
                CoffeeCupBehavior finishedCup = orders.GetChild(i).GetComponent<CoffeeCupBehavior>();
                OrderLogic currentCup = cup.GetComponent<OrderLogic>();
                currentCup.originalCup = finishedCup;
                currentCup.ChildNumber = i;

                //distinguish image by the droptype
                CoffeeOrderSetup.SetOrder(ref cup, finishedCup.DistinguishedMenuName);

                //after distinguishing, deactivate original cup
                finishedCup.gameObject.SetActive(false);

                //set Transform (to not stack in one place)
                RectTransform rt = cup.GetComponent<RectTransform>();
                float gap = CalculateGap(rt);

                rt.Translate(xPos, 0, 0);
                xPos += gap;
            }
        }
    }

    public void DeleteOrder (int ChildNumber)
    {
        OrderLogic order = OrderIcon.GetChild(ChildNumber).GetComponent<OrderLogic>();
        DestroyObject(order.originalCup.gameObject);
        DestroyObject(order.gameObject);
    }

    float CalculateGap (RectTransform rt)
    {
        return (rt.localToWorldMatrix * rt.sizeDelta).x + 20;
    }
}
