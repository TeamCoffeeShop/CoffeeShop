using UnityEngine;
using System.Collections;

public class CoffeeCupSelector : MonoBehaviour
{
    OutlineHighlighter h;
    public GameObject CoffeeCupPrefab;

    void Awake ()
    {
        h = GetComponent<OutlineHighlighter>();
    }

    //if coffeecup is not selected, try to select one
    void Update ()
    {
        if (MinigameManager.Get.CoffeeManager.SelectedCoffee == null)
            h.highlightOn = OutlineHighlighter.HighlightOn.alwaysAndOver;
        else
            h.highlightOn = OutlineHighlighter.HighlightOn.none;
    }

    void OnMouseUp ()
    {
        //if the cup is selected
        if (MinigameManager.Get.CoffeeManager.SelectedCoffee == null)
        {
            //create new cup
            GameObject cup = GameObject.Instantiate(CoffeeCupPrefab);
            MinigameManager.Get.CoffeeManager.SelectedCoffee = cup;
            MinigameManager.Get.CoffeeManager.LockToCamera = true;
            MinigameManager.Get.CoffeeManager.step = 1;
            cup.transform.position = gameObject.transform.position;
        }
    }
}
