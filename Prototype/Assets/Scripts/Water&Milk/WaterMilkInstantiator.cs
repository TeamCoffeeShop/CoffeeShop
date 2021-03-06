﻿using UnityEngine;
using System.Collections;

public class WaterMilkInstantiator : MonoBehaviour
{
    public HotIceType HotIceType;
    public WaterMilkType WaterMilkType;
    public bool MilkFoam = false;
    public bool Ready = false;

    public float MaxAmount = 1000;
    public float CurrentAmount = 0;
    public float IncreaseAmount = 1;

    //Ice
    private GameObject Ice;
    private int iceMax = 5;
    private int numOfIce = 0;

    //Steam
    private GameObject steam;

    public Transform water;
    public Transform milk;

    //Cup
    private bool CoffeeCupCheck = false;
    public CoffeeCupBehavior cup;

    void Update ()
    {
        //stick handle into position
        if (cup != null)
        {
            cup.transform.position = transform.position + new Vector3(0, -3.5f, 0);
        }
    }

    void OnMouseDrag ()
    {
        //if no cup, return
        if (cup == null)
            return;

        switch (HotIceType)
        {
            case HotIceType.Hot:
                if(!steam)
                {
                    steam = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/WhiteSmoke"));
                    steam.transform.position = transform.position;
                
                }
                break;
            case HotIceType.Ice:
                if (numOfIce < iceMax)
                {
                    Ice = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Ice"));
                    Ice.transform.position = transform.position;
                    Ice.transform.Translate(new Vector3(0, -0.5f, 0));
                    numOfIce += 1;
                }
                break;
            default:
                break;
        }

        //stop increasing when max.
        if(CurrentAmount < MaxAmount)
        {
            switch (WaterMilkType)
            {
                case WaterMilkType.Water:
                    //water showoff
                    if (!water)
                        water = (GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Water"), transform.position + new Vector3(0, -0.16f, 0), Quaternion.identity) as GameObject).transform;
                    CurrentAmount += IncreaseAmount;
                    break;
                case WaterMilkType.Milk:
                    //water showoff
                    if (!milk)
                        milk = (GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Milk"), transform.position + new Vector3(0, -0.16f, 0), Quaternion.identity) as GameObject).transform;
                    CurrentAmount += IncreaseAmount;
                    break;
                default:
                    break;
            }

            if (CurrentAmount > MaxAmount)
                CurrentAmount = MaxAmount;
        }
    }

    void OnMouseUp()
    {
        //save the amount to the cup
        if (cup != null)
        {
            if (CurrentAmount <= MaxAmount)
            {
                cup.WaterMilkType = WaterMilkType;
                cup.HotIceType = HotIceType;
                cup.WaterMilkLevel = GetComponent<WaterMilkLevel>().Level;
            }
        }

        if(water != null)
            //finish water
            if (WaterMilkType == WaterMilkType.Water)
            {
                water.GetComponent<WaterFallingLogic>().filling = false;
                water = null;
            }
        if(milk != null)
            if(WaterMilkType == WaterMilkType.Milk)
            {
                milk.GetComponent<WaterFallingLogic>().filling = false;
                milk = null;
            }

        if (steam != null)
            if (HotIceType == HotIceType.Hot)
            {
                Destroy(steam);
            }

    }

    public bool PutCoffeeIntoInstantiator (CoffeeCupBehavior Cup)
    {
        if (CoffeeCupCheck)
            return false;

        CoffeeCupCheck = true;
        cup = Cup;

        //move camera to next one
        Vector3 newPos = Camera.main.transform.position;
        newPos.x = transform.position.x;
        MinigameManager.Get.MakeOrderCamera.SetTargetLocation(newPos);

        return true;
    }

    public bool TakeOutCoffeeCupFromInstantiator ()
    {
        if (cup == null)
            return false;

        CoffeeCupCheck = false;
        cup.GetComponent<OutlineHighlighter>().active = true;
        cup.GetComponent<DragandDrop>().active = true;
        cup = null;

        //reset instantiator
        CurrentAmount = 0;
        numOfIce = 0;

        return true;
    }
}
