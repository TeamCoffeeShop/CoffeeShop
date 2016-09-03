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
            CM.SaveFinishedOrder();
        }
	}

    public void GoBackToMainLevel ()
    {
        CM.SaveFinishedOrder();
        SceneManager.LoadScene(Scenes.MainLevel);
    }
}
