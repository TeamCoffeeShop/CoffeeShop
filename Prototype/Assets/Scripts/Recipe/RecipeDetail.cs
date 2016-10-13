using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecipeDetail : MonoBehaviour
{
    RecipeList CurrentCoffee;
    public Texture[] Coffeebean = new Texture[4];

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        CurrentCoffee = GameObject.Find("MinigameManager").GetComponent<RecipeManager>().CurrentCoffee;
        CheckDetail(CurrentCoffee);	
	}

    public void CheckDetail(RecipeList currentcoffee)
    {
        switch (currentcoffee)
        {
            case RecipeList.HotAmericano:
                gameObject.transform.FindChild("RecipeImage").GetComponent<RawImage>().texture = Coffeebean[0];
                gameObject.transform.FindChild("RecipeText").GetComponent<Text>().text = "Coffee Bean 1 70% ~ 100%" + "\nHot Water 70 % ~ 100 % ";
                break;
            case RecipeList.ColdAmericano:
                gameObject.transform.FindChild("RecipeImage").GetComponent<RawImage>().texture = Coffeebean[1];
                gameObject.transform.FindChild("RecipeText").GetComponent<Text>().text = "Coffee Bean 1 70% ~ 100%" + "\nCold Water 50 % ~ 80 % ";
                break;
            case RecipeList.HotLatte:
                gameObject.transform.FindChild("RecipeImage").GetComponent<RawImage>().texture = Coffeebean[2];
                gameObject.transform.FindChild("RecipeText").GetComponent<Text>().text = "Coffee Bean 2 70% ~ 100%" + "\nHot Milk 60 % ~ 90 %" + "\nMilk Foam";
                break;
            case RecipeList.ColdLatte:
                gameObject.transform.FindChild("RecipeImage").GetComponent<RawImage>().texture = Coffeebean[3];
                gameObject.transform.FindChild("RecipeText").GetComponent<Text>().text = "Coffee Bean 2 70% ~ 100%" + "\nCold Milk 40 % ~ 70 % ";
                break;
        }
    }
}
