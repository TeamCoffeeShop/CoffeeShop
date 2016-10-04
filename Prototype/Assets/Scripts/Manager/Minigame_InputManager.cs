using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Minigame_InputManager : MonoBehaviour
{
    //Image RecipeObject;
    bool RecipeStatus = false;

    void Awake()
    {
        //RecipeObject = GameObject.Find("Recipe").GetComponent<Image>();
    }

    void Update ()
    {
        if (RecipeStatus == true)
        {
            //RecipeObject.enabled = true;
        }
        else if (RecipeStatus == false)
        {
            //RecipeObject.enabled = false;
        }

        //press ESC to go back to mainlevel
        //if(Input.GetKeyDown("escape"))
        //{
        //    GoBackToMainLevel();
        //}

        //temporary. erase this code after creating correct order creation
        if (Input.GetKeyDown("space"))
        {
            MinigameManager.Get.CoffeeManager.SaveFinishedOrder();
        }
	}

    public void GoBackToMainLevel ()
    {
        MinigameManager.Get.CoffeeManager.SaveFinishedOrder();
        SceneManager.LoadScene(Scenes.MainLevel);
        Cursor.visible = true;
    }

    public void Recipe()
    {
        RecipeStatus = !RecipeStatus;
    }

}
