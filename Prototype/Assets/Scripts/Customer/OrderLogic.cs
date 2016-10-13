using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class OrderLogic : MonoBehaviour
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

    public void DragSelectedCup()
    {
        dragging = true;

        //drag the object
        rt.position = Input.mousePosition;
        Vector3 size = rt.localToWorldMatrix * rt.sizeDelta;
        rt.position -= new Vector3(size.x * 0.5f, size.y * 0.5f, 0);
    }

    public void EndDraggingCup()
    {
        dragging = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20, Interactable))
        {
            if(hit.collider.transform.parent != null)
                if (hit.collider.transform.parent.tag == "Customer")
                {
                    CustomerLogic customer = hit.collider.transform.parent.GetComponent<CustomerLogic>();
                    if (type == customer.Order)
                        FinishOrder(customer, true);
                    //else
                    //    GivePenalty(hit.collider.gameObject);
                }
        }


        //throw away
        //if (trash)
        //    MainGameManager.Get.Canvas_OrderHUD.DeleteOrder(ChildNumber);
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

    void FinishOrder(CustomerLogic customer, bool correctOrder)
    {
        float price = CoffeeOrderSetup.PriceTagForMenu(customer.Order);
        float xp = CoffeeOrderSetup.XPForMenu(customer.Order);

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

        customer.LeaveCoffeeShop();
        
        //delete coffee
        MainGameManager.Get.Canvas_OrderHUD.DeleteOrder(ChildNumber);
        DestroyObject(gameObject);
    }

    //void GivePenalty(GameObject orderUI)
    //{
    //    int xp = 5;
    //    int price = 200;

    //    if (customer)
    //    {
    //        //increase xp
    //        MainGameManager.Get.playerManager.SubtractXP(xp);

    //        //increase money
    //        MainGameManager.Get.playerManager.SubtractMoneyGradully(price);
    //        //popup
    //        Text popup = UIEffect.CPopUp(price, transform.position + new Vector3(0, GetComponent<RectTransform>().sizeDelta.y + 10, 0)).GetComponent<Text>();
    //        UIEffect.SetPopUpBehavior(popup, PopupType.penalty);

    //        //delete coffee
    //        OrderLogic logic = orderUI.GetComponent<OrderLogic>();
    //        if (logic)
    //            MainGameManager.Get.Canvas_OrderHUD.DeleteOrder(logic.ChildNumber);
    //        DestroyObject(orderUI);

    //        customer.GetComponent<CustomerLogic>().LeaveCoffeeShop();

    //        Cursor.visible = true;
    //    }

    //}
}
