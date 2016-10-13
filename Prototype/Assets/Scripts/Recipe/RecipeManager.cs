using UnityEngine;
using System.Collections;

public enum RecipeStat
{
    off, on
}

public enum RecipeDetailStat
{
    Deoff, Deon
}

public enum RecipeList
{
    HotAmericano, ColdAmericano, HotLatte, ColdLatte
}

public class RecipeManager : MonoBehaviour
{
    public GameObject RecipeBook;
    public GameObject RecipeDetail;
    public GameObject RecipeButton;

    public RecipeList CurrentCoffee;

    //no change!
    RecipeStat CurrentMode = RecipeStat.on;
    RecipeDetailStat CurrentDetailMode = RecipeDetailStat.Deon;

    // Use this for initialization
    void Start()
    {
        //no change!
        RecipeBook.SetActive(false);
        RecipeDetail.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RecipeToggle()
    {
        TurnOnOff(CurrentMode);
    }

    public void TurnOnOff(RecipeStat mode)
    {
        switch (mode)
        {
            case RecipeStat.off:
                RecipeDetail.SetActive(false);
                CurrentDetailMode = RecipeDetailStat.Deon;
                RecipeBook.SetActive(false);
                CurrentMode = RecipeStat.on;
                break;
            case RecipeStat.on:
                RecipeBook.SetActive(true);
                CurrentMode = RecipeStat.off;
                break;
        }
    }

    public void Close()
    {
        RecipeDetail.SetActive(false);
        CurrentDetailMode = RecipeDetailStat.Deon;
        RecipeBook.SetActive(false);
        CurrentMode = RecipeStat.on;        
    }

    public void RecipeDetailToggle()
    {
        TurnOnOffDetail(CurrentDetailMode);
    }

    public void TurnOnOffDetail(RecipeDetailStat mode)
    {
        switch (mode)
        {            
            case RecipeDetailStat.Deoff:
                RecipeDetail.SetActive(false);
                CurrentDetailMode = RecipeDetailStat.Deon;
                break;
            case RecipeDetailStat.Deon:
                RecipeDetail.SetActive(true);
                CurrentDetailMode = RecipeDetailStat.Deoff;
                break;
        }
    }

    public void DetailClose()
    {
        RecipeDetail.SetActive(false);
        CurrentDetailMode = RecipeDetailStat.Deon;
    }
}
