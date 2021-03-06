﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum Mode
{
    off, on
}

public class CoffeeBeanInstantiator : MonoBehaviour
{
    public float WaitingTime = 3.0f;

    public Mode IsMode;
    public GameObject CoffeeBean1;
    public GameObject CoffeeBean2;
    public GameObject CoffeeBean3;
    public GameObject CoffeeBean4;
    public bool CoffeeBean1Ready;
    public bool CoffeeBean2Ready;
    public bool CoffeeBean3Ready;
    public bool CoffeeBean4Ready;
    GameObject CurrentCoffeeBean;

    //shake variables
    Vector3 OrigianlPosition;
    bool ShakeReady;
    bool ShakeCheck;
    float ShakeDistance = 0.1f;

    //highlight
    OutlineHighlighter h;

    public GameObject arrow;
    void Awake ()
    {
        h = GetComponent<OutlineHighlighter>();
        arrow.SetActive(false);
    }

    void Start()
    {
        SetMode(Mode.off);
        OrigianlPosition = gameObject.transform.position;
    }

    void Update()
    {
        if (ShakeReady)
        {
            arrow.SetActive(false);
            h.highlightOn = OutlineHighlighter.HighlightOn.none;
            Shake();
        }
        else
        {
            if (MinigameManager.Get.CoffeeManager.IsMakingOrderJustStarted)
            {
                h.highlightOn = OutlineHighlighter.HighlightOn.alwaysAndOver;
                arrow.SetActive(true);
            }
            else
            {
                h.highlightOn = OutlineHighlighter.HighlightOn.none;
                arrow.SetActive(false);
            }

            gameObject.transform.position = OrigianlPosition;

            if (CoffeeBean1Ready)
                MinigameManager.Get.Canvas_UI.transform.FindChild("CoffeeBeanSelection").transform.FindChild("CoffeeBean1").GetComponent<RawImage>().enabled = true;
            else
                MinigameManager.Get.Canvas_UI.transform.FindChild("CoffeeBeanSelection").transform.FindChild("CoffeeBean1").GetComponent<RawImage>().enabled = false;
            if (CoffeeBean2Ready)
                MinigameManager.Get.Canvas_UI.transform.FindChild("CoffeeBeanSelection").transform.FindChild("CoffeeBean2").GetComponent<RawImage>().enabled = true;
            else
                MinigameManager.Get.Canvas_UI.transform.FindChild("CoffeeBeanSelection").transform.FindChild("CoffeeBean2").GetComponent<RawImage>().enabled = false;
            if (CoffeeBean3Ready)
                MinigameManager.Get.Canvas_UI.transform.FindChild("CoffeeBeanSelection").transform.FindChild("CoffeeBean3").GetComponent<RawImage>().enabled = true;
            else
                MinigameManager.Get.Canvas_UI.transform.FindChild("CoffeeBeanSelection").transform.FindChild("CoffeeBean3").GetComponent<RawImage>().enabled = false;
            if (CoffeeBean4Ready)
                MinigameManager.Get.Canvas_UI.transform.FindChild("CoffeeBeanSelection").transform.FindChild("CoffeeBean4").GetComponent<RawImage>().enabled = true;
            else
                MinigameManager.Get.Canvas_UI.transform.FindChild("CoffeeBeanSelection").transform.FindChild("CoffeeBean4").GetComponent<RawImage>().enabled = false;
        }
    }

    public void ToggleMode()
    {
        if (!ShakeReady)
        {
            SetMode(Mode.on);
        }
    }

    public void SetMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.off:
                MinigameManager.Get.Canvas_UI.transform.FindChild("CoffeeBeanSelection").GetComponent<CoffeeBeanSelection>().Off();
                break;
            case Mode.on:
                MinigameManager.Get.Canvas_UI.transform.FindChild("CoffeeBeanSelection").GetComponent<CoffeeBeanSelection>().On();
                break;
        }
        IsMode = mode;
    }

    void OnMouseDown()
    {
        ToggleMode();
    }

    public void SelectCoffeeBean(int WhatBean)
    {
        switch (WhatBean)
        {
            case 1:
                CurrentCoffeeBean = CoffeeBean1;
                break;
            case 2:
                CurrentCoffeeBean = CoffeeBean2;
                break;
            case 3:
                CurrentCoffeeBean = CoffeeBean3;
                break;
            case 4:
                CurrentCoffeeBean = CoffeeBean4;
                break;
        }
        StartCoroutine(MakeCoffeeBean());
        SetMode(Mode.off);
    }

    public void CancleCoffeeBeanSelection()
    {
        SetMode(Mode.off);
    }

    IEnumerator MakeCoffeeBean()
    {
        ShakeReady = true;
        yield return new WaitForSeconds(WaitingTime);
        ShakeReady = false;
        GameObject coffeebean = (GameObject)Instantiate(CurrentCoffeeBean, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        coffeebean.GetComponent<DragandDrop>().Highlight[0] = MinigameManager.Get.handGrinder.GetComponent<OutlineHighlighter>();
        coffeebean.GetComponent<DragandDrop>().Target[0] = MinigameManager.Get.handGrinder.transform.FindChild("Collider").gameObject;
        MinigameManager.Get.CoffeeManager.IsMakingOrderJustStarted = false;
    }

    void Shake()
    {
        if (ShakeCheck)
        {
            gameObject.transform.position += new Vector3(ShakeDistance, 0, 0);
            ShakeCheck = !ShakeCheck;
        }
        else
        {
            gameObject.transform.position += new Vector3(-ShakeDistance, 0, 0);
            ShakeCheck = !ShakeCheck;
        }
    }
}
