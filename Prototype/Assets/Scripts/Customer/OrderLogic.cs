using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OrderLogic : MonoBehaviour
{
    public OrderType type = OrderType.None;
    public float SelectCancelSpeed = 3;
    public int ChildNumber;
    public Vector3 OriginalPosition;

    bool dragging = false;
    bool trash = false;
    RectTransform rt;
    float reactionTime;
    float totalReactionTime = 0.5f;

    void Awake ()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }

    public void DragSelectedCup()
    {
        dragging = true;
        Cursor.visible = false;


        //drag the object
        rt.position = Input.mousePosition;
        Vector3 size = rt.localToWorldMatrix * rt.sizeDelta;
        rt.position -= new Vector3(size.x * 0.5f, size.y * 0.5f, 0);
    }

    public void EndDraggingCup()
    {
        Cursor.visible = true;
        dragging = false;

        //throw away
        if (trash)
            MainGameManager.Get.Canvas_OrderHUD.DeleteOrder(ChildNumber);
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

    void OnTriggerEnter2D(Collider2D orderUI)
    {
        if(orderUI.name == "Trash")
           trash = true;
    }

    void OnTriggerExit2D(Collider2D orderUI)
    {
        if (orderUI.name == "Trash")
            trash = false;
    }
}
