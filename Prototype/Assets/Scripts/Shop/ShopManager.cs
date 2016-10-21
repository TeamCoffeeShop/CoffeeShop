using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour
{
    public ShopSelection DecoPanel;
    public ShopSelection ShopPanel;
    public ShopSelection RecipePanel;

    void Start()
    {
        gameObject.SetActive(false);
        DecoPanel.On();
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    //diary section selection
    public void ShopSelectSection(int WhatSection)
    {
        DecoPanel.Off();
        ShopPanel.Off();
        RecipePanel.Off();
        switch (WhatSection)
        {
            case 1:
                DecoPanel.On();
                break;
            case 2:
                ShopPanel.On();
                break;
            case 3:
                RecipePanel.On();
                break;
        }
    }
}
