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
            SceneManager.LoadScene(Scenes.MainLevel);
            Cursor.visible = true;
        }

        //temporary. erase this code after creating correct order creation
        if(Input.GetKeyDown("space"))
        {
            Transform list = GameObject.Find("[[Finished Orders]]").transform;
            if (list != null && CM.SelectedCoffee != null)
            {
                Debug.Log("coffee successfully added to list!");
                CM.SelectedCoffee.transform.parent = list;
                CM.SelectedCoffee = null;
            }
        }
	}
}
