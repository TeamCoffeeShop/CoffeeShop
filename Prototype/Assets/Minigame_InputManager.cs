using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Minigame_InputManager : MonoBehaviour
{	
	void Update ()
    {
	    if(Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene(2);
        }
	}
}
