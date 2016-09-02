using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Minigame_InputManager : MonoBehaviour
{
    Minigame_CoffeeManager CM;

    void Awake()
    {
        CM = GameObject.Find("Manager").transform.Find("CoffeeManager").GetComponent<Minigame_CoffeeManager>();
    }

	void Update ()
    {
        //press ESC to go back to mainlevel
	    if(Input.GetKeyDown("escape"))
        {
            GoBackToMainLevel();
            Cursor.visible = true;
        }

        //temporary. erase this code after creating correct order creation
        if(Input.GetKeyDown("space"))
        {
            SaveFinishedOrder();
        }
	}

    public void GoBackToMainLevel ()
    {
        SaveFinishedOrder();
        SceneManager.LoadScene(Scenes.MainLevel);
    }

    public void SaveFinishedOrder ()
    {
        GameObject orders = GameObject.Find("[[Finished Orders]]");

        if(orders)
        {
            Transform list = orders.transform;
            if (list != null && CM.SelectedCoffee != null)
            {
                //check if coffee is finished
                if (LegitCoffee())
                {
                    Debug.Log("coffee successfully added to list!");
                    CM.SelectedCoffee.transform.parent = list;
                    CM.SelectedCoffee.SetActive(false);
                    CM.SelectedCoffee = null;
                }

            }
        }
    }

    //check if the coffee is legit
    bool LegitCoffee()
    {
        CoffeeCupBehavior cup = CM.SelectedCoffee.GetComponent<CoffeeCupBehavior>();

        if (cup.DropType == CoffeeDropType.None)
            return false;

        return true;
    }
}
