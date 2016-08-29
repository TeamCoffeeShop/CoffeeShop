using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Minigame_InputManager : MonoBehaviour
{	
	void Update ()
    {
        //press ESC to go back to mainlevel
	    if(Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene(Scenes.MainLevel);
            Cursor.visible = true;
        }

        //temporary. erase this code after creating correct order creation
        //if(Input.GetKeyDown("space"))
        //{
        //    GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Cup"), new Vector3(0, 0, 0), Quaternion.identity);
        //}
	}
}
