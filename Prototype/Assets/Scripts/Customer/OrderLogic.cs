﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OrderLogic : MonoBehaviour
{
    public OrderType type = OrderType.None;
    public float SelectCancelSpeed = 5;
    public int ChildNumber;

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
            //return to its original position
            rt.position += (OriginalPosition - rt.position) * Time.deltaTime * SelectCancelSpeed;
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
