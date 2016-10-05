using UnityEngine;
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

    void Awake ()
    {
        h = GetComponent<OutlineHighlighter>();
    }

    void Start()
    {
        SetMode(Mode.off);
        OrigianlPosition = gameObject.transform.position;
    }

    void Update()
    {
        //if (MinigameManager.Get.CoffeeManager.step == 1)
        //    h.highlightOn = OutlineHighlighter.HighlightOn.alwaysAndOver;
        //else
        //    h.highlightOn = OutlineHighlighter.HighlightOn.none;

        if (ShakeReady)
            Shake();
        else
        {
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
        if(!ShakeReady)
            SetMode(Mode.on);
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
        GameObject coffeeBean = (GameObject)Instantiate(CurrentCoffeeBean, transform.position - new Vector3(0, 1, 0), Quaternion.identity);
        MinigameManager.Get.CoffeeManager.step = 2;
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
