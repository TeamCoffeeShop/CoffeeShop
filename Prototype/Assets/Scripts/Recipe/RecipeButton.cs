using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecipeButton : MonoBehaviour
{
    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        
        if (yourButton.name == "HotAmericano")
            GameObject.Find("MinigameManager").GetComponent<RecipeManager>().CurrentCoffee = RecipeList.HotAmericano;
        if (yourButton.name == "ColdAmericano")
            GameObject.Find("MinigameManager").GetComponent<RecipeManager>().CurrentCoffee = RecipeList.ColdAmericano;
        if (yourButton.name == "HotLatte")
            GameObject.Find("MinigameManager").GetComponent<RecipeManager>().CurrentCoffee = RecipeList.HotLatte;
        if (yourButton.name == "ColdLatte")
            GameObject.Find("MinigameManager").GetComponent<RecipeManager>().CurrentCoffee = RecipeList.ColdLatte;
    }
}
