using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OrderLogic : MonoBehaviour
{
    public float SelectCancelSpeed = 5;
    public CoffeeCupBehavior originalCup;
    public FinishedOrderList OrderManager;
    public OrderLogic NextFinishedOrder;
    public float Gap;

    Vector3 OriginalPosition;
    bool dragging = false;
    bool trash = false;
    RectTransform rt;

    void Awake ()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }

    void Start ()
    {
        OriginalPosition = rt.position;
    }

    public void DragSelectedCup()
    {
        dragging = true;
        Cursor.visible = false;


        //drag the object
        rt.position = Input.mousePosition;
        Vector3 size = rt.localToWorldMatrix * rt.sizeDelta;
        rt.position -= new Vector3(size.x * 0.5f, size.y * 0.5f, 0);

        //show trash can
        OrderManager.SetTrashVisible(true);
    }

    public void EndDraggingCup()
    {
        Cursor.visible = true;
        dragging = false;

        //throw away
        if (trash)
        {
            NextFinishedOrder.MoveLeft();
            DestroyObject(originalCup.gameObject);
            DestroyObject(this.gameObject);
        }

        //disable trash can
        OrderManager.SetTrashVisible(false);
    }

    void Update ()
    {
        if(!dragging)
        {
            //return to its original position
            rt.position += (OriginalPosition - rt.position) * Time.deltaTime * SelectCancelSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D orderUI)
    {
        trash = true;
    }

    void OnTriggerExit2D(Collider2D orderUI)
    {
        trash = false;
    }

    //move this to left when order is gone
    public void MoveLeft ()
    {
        OriginalPosition.x -= Gap;

        if (NextFinishedOrder)
            NextFinishedOrder.MoveLeft();
    }
}
