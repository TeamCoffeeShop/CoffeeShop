using UnityEngine;
using System.Collections;

public enum RecipeStat
{
    off, on
}

public enum RecipeList
{
    HotAmericano, IcedAmericano, HotLatte, IcedLatte
}

public class RecipeManager : MonoBehaviour
{
    public GameObject RecipeBook;
    public GameObject RecipeButton;

    //no change!
    RecipeStat CurrentMode = RecipeStat.on;

    // Use this for initialization
    void Start()
    {
        //no change!
        RecipeBook.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Close()
    {
        RecipeBook.SetActive(false);
        CurrentMode = RecipeStat.on;
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
                RecipeBook.SetActive(false);
                CurrentMode = RecipeStat.on;
                break;
            case RecipeStat.on:
                RecipeBook.SetActive(true);
                CurrentMode = RecipeStat.off;
                break;
        }
    }

    public void ShowRecipe()
    {

    }
}
