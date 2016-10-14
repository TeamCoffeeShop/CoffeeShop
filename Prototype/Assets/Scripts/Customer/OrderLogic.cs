using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class OrderLogic : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public OrderType type = OrderType.None;
    public float SelectCancelSpeed = 3;
    public int ChildNumber;
    public Vector3 OriginalPosition;

    //bool trash = false;
    RectTransform rt;
    float reactionTime;
    float totalReactionTime = 0.5f;
    bool dragging = false;
    private LayerMask Interactable;

    void Awake ()
    {
        rt = gameObject.GetComponent<RectTransform>();
        Interactable = LayerMask.GetMask("Interactable");
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragging = true;

        //drag the object
        rt.position = eventData.position;// Input.mousePosition;
        Vector3 size = rt.localToWorldMatrix * rt.sizeDelta;
        rt.position -= new Vector3(size.x * 0.5f, size.y * 0.5f, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;

        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Interactable))
        {
            if (hit.collider.transform.parent != null)
                if (hit.collider.transform.parent.tag == "Customer")
                {
                    Customer customer = hit.collider.transform.parent.GetComponent<Customer>();
                    FinishOrder(customer, type == customer.data.order);
                }
        }

        //throw away
        //if (trash)
           // MainGameManager.Get.Canvas_OrderHUD.DeleteOrder(ChildNumber);
    }

    void Update ()
    {
        if(!dragging)
        {
            Vector3 dir = OriginalPosition - rt.position;

            if (reactionTime > totalReactionTime)
                //return to its original position
                rt.position += dir * Time.deltaTime * SelectCancelSpeed;
            else
                rt.position += dir * Time.deltaTime * reactionTime / totalReactionTime * SelectCancelSpeed;

            reactionTime += Time.deltaTime;
        }
    }

    void FinishOrder(Customer customer, bool correctOrder)
    {
        float price;
        float xp;

        if (correctOrder)
        {
            price = CoffeeOrderSetup.PriceTagForMenu(customer.data.order);
            xp = CoffeeOrderSetup.XPForMenu(customer.data.order);

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

            customer.GetComponent<CustomerLogic>().LeaveCoffeeShop();

            //delete coffee
            MainGameManager.Get.Canvas_OrderHUD.DeleteOrder(ChildNumber);
            DestroyObject(gameObject);
        }
        else
        {
            price = 200;
            xp = 5;

            //increase xp
            MainGameManager.Get.playerManager.SubtractXP(xp);

            //increase money
            MainGameManager.Get.playerManager.SubtractMoneyGradully(price);
            //popup
            Text popup = UIEffect.CPopUp(price, transform.position + new Vector3(0, GetComponent<RectTransform>().sizeDelta.y + 10, 0)).GetComponent<Text>();
            UIEffect.SetPopUpBehavior(popup, PopupType.penalty);

            customer.GetComponent<CustomerLogic>().LeaveCoffeeShop();

            //delete coffee
            MainGameManager.Get.Canvas_OrderHUD.DeleteOrder(ChildNumber);
            DestroyObject(gameObject);
        }


    }
}
