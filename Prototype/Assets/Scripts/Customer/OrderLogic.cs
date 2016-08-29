using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OrderLogic : MonoBehaviour
{
    public bool CompletedOrder = false;
    public float SelectCancelSpeed = 5;

    Vector3 OriginalPosition;
    bool dragging = false;
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
    }

    public void EndDraggingCup()
    {
        Cursor.visible = true;
        dragging = false;
    }

    void Update ()
    {
        if(!dragging)
        {
            //return to its original position
            rt.position += (OriginalPosition - rt.position) * Time.deltaTime * SelectCancelSpeed;
        }
    }
}
